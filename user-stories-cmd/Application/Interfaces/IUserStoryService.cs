using Application.DTOs;

namespace Application.Interfaces;

public interface IUserStoryService
{
    Task<Result<CreatedUserStoryDTO>> Create(CreateUserStoryDTO usDTO);
}