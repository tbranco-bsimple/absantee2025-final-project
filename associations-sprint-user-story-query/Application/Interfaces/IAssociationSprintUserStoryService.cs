using Application.DTOs;

namespace Application.Interfaces;

public interface IAssociationSprintUserStoryService
{
    Task<Result<IEnumerable<AssociationSprintUserStoryDTO>>> GetAll();
    Task AddConsumed(CreateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO);
}