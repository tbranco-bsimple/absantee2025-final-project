using Application.DTOs;

namespace Application.Interfaces;

public interface IUserStoryService
{
    Task<Result<CreatedUserStoryDTO>> Create(CreateUserStoryDTO userStoryDTO);
    Task AddConsumed(CreateUserStoryFromMessageDTO userStoryDTO);
}