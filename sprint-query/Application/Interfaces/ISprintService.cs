using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface ISprintService
{
    Task<Result<IEnumerable<SprintDTO>>> GetAll();
    Task AddConsumed(CreateSprintFromMessageDTO sprintDTO);
}