using Application.DTOs;
using Application.Interfaces;
using Domain.Factory;
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
        };

        var newUserStory = _userStoryFactory.Create(userStoryVisitor);
        await _userStoryRepository.AddAsync(newUserStory);
    }
}