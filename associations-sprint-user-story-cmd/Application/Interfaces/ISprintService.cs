using Application.DTOs;

namespace Application.Interfaces;

public interface ISprintService
{
    Task AddConsumed(CreateSprintFromMessageDTO sprintDTO);
}