using Domain.Models;

namespace Application.DTOs;

public record CreatedUserStoryDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Risk Risk { get; set; }

    public CreatedUserStoryDTO(Guid id, string description, Priority priority, Risk risk)
    {
        Id = id;
        Description = description;
        Priority = priority;
        Risk = risk;
    }
}