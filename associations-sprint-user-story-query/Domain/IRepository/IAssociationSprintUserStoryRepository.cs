using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationSprintUserStoryRepository
{
    Task<IAssociationSprintUserStory> AddAsync(IAssociationSprintUserStory associationSprintUserStory);
    Task<IEnumerable<IAssociationSprintUserStory>> GetAllAsync();
    Task<IAssociationSprintUserStory?> GetByIdAsync(Guid id);
}