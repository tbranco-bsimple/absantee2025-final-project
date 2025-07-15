using Domain.Interfaces;

namespace Domain.IRepository;

public interface IProjectRepository
{
    Task<IProject> AddAsync(IProject project);
    Task<IProject?> GetByIdAsync(Guid id);
}