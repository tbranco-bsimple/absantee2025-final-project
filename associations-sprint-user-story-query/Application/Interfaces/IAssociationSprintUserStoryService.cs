using Application.DTOs;

namespace Application.Interfaces;

public interface IAssociationSprintUserStoryService
{
    Task<Result<IEnumerable<AssociationSprintUserStoryDTO>>> GetAll();
    Task<Result<IEnumerable<UserStoryDTO>>> GetAllUserStoriesOfSprint(Guid sprintId);
    Task<Result<UserStoryDTO>> GetUserStoryOfSprint(Guid sprintId, Guid userStoryId);
    Task AddConsumed(CreateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
    Task UpdateConsumed(UpdateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
}