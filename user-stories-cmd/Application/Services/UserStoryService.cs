using System.Data.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.IPublishers;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class UserStoryService : IUserStoryService
{
    private IUserStoryRepository _userStoryRepository;
    private IUserStoryFactory _userStoryFactory;
    private readonly IMessagePublisher _publisher;

    public UserStoryService(IUserStoryRepository userStoryRepository, IUserStoryFactory userStoryFactory, IMessagePublisher publisher)
    {
        _userStoryRepository = userStoryRepository;
        _userStoryFactory = userStoryFactory;
        _publisher = publisher;
    }

    public async Task<Result<CreatedUserStoryDTO>> Create(CreateUserStoryDTO userStoryDTO)
    {
        IUserStory newUserStory;
        try
        {
            newUserStory = _userStoryFactory.Create(userStoryDTO.Description, userStoryDTO.Priority, userStoryDTO.Risk);
            newUserStory = await _userStoryRepository.AddAsync(newUserStory);

            var result = new CreatedUserStoryDTO(newUserStory.Id, newUserStory.Description, newUserStory.Priority, newUserStory.Risk);

            await _publisher.PublishUserStoryCreatedAsync(newUserStory);
            return Result<CreatedUserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<CreatedUserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    /*     public async Task<CreatedUserStoryDTO> Create(CreateUserStoryDTO userStoryDTO)
        {
            IUserStory newUserStory;
                newUserStory = _userStoryFactory.Create(userStoryDTO.Description, userStoryDTO.Priority, userStoryDTO.Risk);
                newUserStory = await _userStoryRepository.AddAsync(newUserStory);

                var result = new CreatedUserStoryDTO(newUserStory.Id, newUserStory.Description, newUserStory.Priority, newUserStory.Risk);

                await _publisher.PublishUserStoryCreatedAsync(newUserStory);

                return result;

        } */

    public async Task AddConsumed(CreateUserStoryFromMessageDTO userStoryDTO)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(userStoryDTO.Id);

        if (userStory != null)
        {
            Console.WriteLine($"UserStoryConsumed not added, already exists with Id: {userStoryDTO.Id}");
            return;
        }

        var userStoryVisitor = new UserStoryDataModel()
        {
            Id = userStoryDTO.Id,
            Description = userStoryDTO.Description,
            Priority = userStoryDTO.Priority,
            Risk = userStoryDTO.Risk,
        };

        var newCollab = _userStoryFactory.Create(userStoryVisitor);
        await _userStoryRepository.AddAsync(newCollab);
    }
}