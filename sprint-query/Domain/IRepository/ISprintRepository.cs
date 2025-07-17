using Domain.Interfaces;

namespace Domain.IRepository;

public interface ISprintRepository
{
    Task<ISprint> AddAsync(ISprint sprint);
    Task<IEnumerable<ISprint>> GetAllAsync();
    Task<ISprint?> GetByIdAsync(Guid id);
}