namespace Application.DTOs;

public record CreateUserStoryFromMessageDTO
{
    public Guid Id { get; set; }

    public CreateUserStoryFromMessageDTO(Guid id)
    {
        Id = id;
    }
}