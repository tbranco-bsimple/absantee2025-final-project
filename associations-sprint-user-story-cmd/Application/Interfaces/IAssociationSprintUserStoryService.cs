using Application.DTOs;

namespace Application.Interfaces;

public interface IAssociationSprintUserStoryService
{
    Task<Result<CreatedAssociationSprintUserStoryDTO>> Create(CreateAssociationSprintUserStoryDTO associationSprintUserStoryDTO);
    Task<Result<UpdatedAssociationSprintUserStoryDTO>> UpdateEffortCompletion(Guid id, UpdateEffortAndCompletionDTO updateEffortAndCompletionDTO);
    Task AddConsumed(CreateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
    Task UpdateConsumed(UpdateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
}