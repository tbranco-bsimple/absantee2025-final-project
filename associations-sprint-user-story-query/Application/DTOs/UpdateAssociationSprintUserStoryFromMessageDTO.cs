namespace Application.DTOs;

public record UpdateAssociationSprintUserStoryFromMessageDTO
{
    public Guid Id { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }

    public UpdateAssociationSprintUserStoryFromMessageDTO(Guid id, int effortHours, int completionPercentage)
    {
        Id = id;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }
}