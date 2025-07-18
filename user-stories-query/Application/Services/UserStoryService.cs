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

    public UserStoryService(IUserStoryRepository userStoryRepository, IUserStoryFactory userStoryFactory)
    {
        _userStoryRepository = userStoryRepository;
        _userStoryFactory = userStoryFactory;
    }

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

        var newUserStory = _userStoryFactory.Create(userStoryVisitor);
        await _userStoryRepository.AddAsync(newUserStory);
    }

    public async Task<Result<IEnumerable<UserStoryDTO>>> GetAll()
    {
        try
        {
            var userStories = await _userStoryRepository.GetAllAsync();
            var result = userStories.Select(us => new UserStoryDTO(us.Id, us.Description, us.Priority, us.Risk));

            return Result<IEnumerable<UserStoryDTO>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IEnumerable<UserStoryDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<UserStoryDTO>> GetById(Guid id)
    {
        try
        {
            var us = await _userStoryRepository.GetByIdAsync(id);
            if (us == null)
                return Result<UserStoryDTO>.Failure(Error.NotFound($"User Story with ID {id} not found."));

            var result = new UserStoryDTO(us.Id, us.Description, us.Priority, us.Risk);

            return Result<UserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<UserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }
}