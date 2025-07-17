namespace Application.DTOs;

public record CreateAssociationSprintUserStoryDTO
{
    public Guid SprintId { get; set; }
    public Guid UserStoryId { get; set; }
    public Guid CollaboratorId { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }
}