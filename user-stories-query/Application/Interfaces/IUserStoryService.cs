using Application.DTOs;

namespace Application.Interfaces;

public interface IUserStoryService
{
    Task<Result<IEnumerable<UserStoryDTO>>> GetAll();
    Task AddConsumed(CreateUserStoryFromMessageDTO userStoryDTO);
}