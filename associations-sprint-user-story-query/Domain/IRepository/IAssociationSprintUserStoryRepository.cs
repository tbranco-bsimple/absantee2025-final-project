using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationSprintUserStoryRepository
{
    Task<IAssociationSprintUserStory> AddAsync(IAssociationSprintUserStory associationSprintUserStory);
    Task<IEnumerable<IAssociationSprintUserStory>> GetAllAsync();
    Task<IEnumerable<IAssociationSprintUserStory>> GetAllBySprintIdAsync(Guid sprintId);
    Task<IAssociationSprintUserStory?> GetBySprintUserStoryAsync(Guid sprintId, Guid userStoryId);
    Task<IAssociationSprintUserStory?> GetByIdAsync(Guid id);
}