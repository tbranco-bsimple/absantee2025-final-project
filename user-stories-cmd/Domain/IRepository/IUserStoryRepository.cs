using Domain.Interfaces;

namespace Domain.IRepository;

public interface IUserStoryRepository
{
    Task<IUserStory> AddAsync(IUserStory userStory);
    Task<IUserStory?> GetByIdAsync(Guid id);
}