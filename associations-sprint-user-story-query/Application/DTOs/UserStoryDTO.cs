namespace Application.DTOs;

public record UserStoryDTO
{
    public Guid Id { get; set; }

    public UserStoryDTO(Guid id)
    {
        Id = id;
    }
}