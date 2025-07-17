using Domain.Interfaces;

namespace Domain.IRepository;

public interface ICollaboratorRepository
{
    Task<ICollaborator> AddAsync(ICollaborator project);
    Task<ICollaborator?> GetByIdAsync(Guid id);
}