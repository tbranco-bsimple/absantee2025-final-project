using Application.DTOs;

namespace Application.Interfaces;

public interface ISprintService
{
    Task<Result<CreatedSprintDTO>> Create(CreateSprintDTO sprintDTO);
    Task AddConsumed(CreateSprintFromMessageDTO sprintDTO);
}