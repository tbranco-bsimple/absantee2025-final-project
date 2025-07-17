using Application.DTOs;

namespace Application.Interfaces;

public interface ISprintService
{
    Task<Result<IEnumerable<SprintDTO>>> GetAll();
    Task<Result<IEnumerable<SprintDTO>>> GetAllByProjectId(Guid projectId);
    Task AddConsumed(CreateSprintFromMessageDTO sprintDTO);
}