using Application.DTOs;

namespace Application.Interfaces;

public interface IAssociationSprintUserStoryService
{
    Task<Result<CreatedAssociationSprintUserStoryDTO>> Create(CreateAssociationSprintUserStoryDTO associationSprintUserStoryDTO);
    Task AddConsumed(CreateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
}