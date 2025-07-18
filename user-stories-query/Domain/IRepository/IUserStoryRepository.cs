using Domain.Interfaces;

namespace Domain.IRepository;

public interface IUserStoryRepository
{
    Task<IUserStory> AddAsync(IUserStory userStory);
    Task<IEnumerable<IUserStory>> GetAllAsync();
    Task<IUserStory?> GetByIdAsync(Guid id);
}