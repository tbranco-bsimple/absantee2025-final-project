using Application.DTOs;
using Application.Interfaces;
/* using Application.IPublishers; */
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services;

public class UserStoryService : IUserStoryService
{
    private IUserStoryRepository _userStoryRepository;
    private IUserStoryFactory _userStoryFactory;
    /* private readonly IMessagePublisher _publisher; */

    public UserStoryService(IUserStoryRepository userStoryRepository, IUserStoryFactory userStoryFactory/*,  IMessagePublisher publisher */)
    {
        _userStoryRepository = userStoryRepository;
        _userStoryFactory = userStoryFactory;
        /*  _publisher = publisher; */
    }

    public async Task<Result<CreatedUserStoryDTO>> Create(CreateUserStoryDTO usDTO)
    {
        IUserStory newUS;
        try
        {
            newUS = _userStoryFactory.Create(usDTO.Description, usDTO.Priority, usDTO.Risk);
            newUS = await _userStoryRepository.AddAsync(newUS);

            var result = new CreatedUserStoryDTO(newUS.Id, newUS.Description, newUS.Priority, newUS.Risk);

            /* await _publisher.PublishUserStoryCreatedAsync(newUS); */
            return Result<CreatedUserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<CreatedUserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }
}