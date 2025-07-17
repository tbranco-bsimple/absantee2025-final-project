using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationSprintUserStoryRepository
{
    Task<IAssociationSprintUserStory> AddAsync(IAssociationSprintUserStory associationSprintUserStory);
    Task<IAssociationSprintUserStory?> GetByIdAsync(Guid id);
}