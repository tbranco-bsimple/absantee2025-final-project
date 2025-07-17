using Application.DTOs;

namespace Application.Interfaces;

public interface IUserStoryService
{
    Task AddConsumed(CreateUserStoryFromMessageDTO userStoryDTO);
}