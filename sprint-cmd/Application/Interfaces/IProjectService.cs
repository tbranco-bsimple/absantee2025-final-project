using Application.DTOs;

namespace Application.Interfaces;

public interface IProjectService
{
    Task AddConsumed(CreateProjectFromMessageDTO projectDTO);
}