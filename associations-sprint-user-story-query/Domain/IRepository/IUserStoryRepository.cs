using Domain.Interfaces;

namespace Domain.IRepository;

public interface IUserStoryRepository
{
    Task<IUserStory> AddAsync(IUserStory sprint);
    Task<IUserStory?> GetByIdAsync(Guid id);
}