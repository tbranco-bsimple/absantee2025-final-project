using Domain.Interfaces;

namespace Domain.IRepository;

public interface ISprintRepository
{
    Task<ISprint> AddAsync(ISprint sprint);
    Task<IEnumerable<ISprint>> GetAllAsync();
    Task<IEnumerable<ISprint>> GetAllByProjectIdAsync(Guid projectId);
    Task<ISprint?> GetByIdAsync(Guid id);
}