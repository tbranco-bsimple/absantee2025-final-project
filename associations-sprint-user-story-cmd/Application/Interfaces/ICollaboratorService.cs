using Application.DTOs;

namespace Application.Interfaces;

public interface ICollaboratorService
{
    Task AddConsumed(CreateCollaboratorFromMessageDTO collaboratorDTO);
}