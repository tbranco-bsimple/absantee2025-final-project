using Application.DTOs;

namespace Application.Interfaces;

public interface IUserStoryService
{
    Task<Result<IEnumerable<UserStoryDTO>>> GetAll();
    Task<Result<UserStoryDTO>> GetById(Guid id);
    Task AddConsumed(CreateUserStoryFromMessageDTO userStoryDTO);
}