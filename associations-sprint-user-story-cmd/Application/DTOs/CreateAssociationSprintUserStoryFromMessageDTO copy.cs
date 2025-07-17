namespace Application.DTOs;

public record CreateAssociationSprintUserStoryFromMessageDTO
{
    public Guid Id { get; set; }
    public Guid SprintId { get; set; }
    public Guid UserStoryId { get; set; }
    public Guid CollaboratorId { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }

    public CreateAssociationSprintUserStoryFromMessageDTO(Guid id, Guid sprintId, Guid userStoryId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        Id = id;
        SprintId = sprintId;
        UserStoryId = userStoryId;
        CollaboratorId = collaboratorId;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }
}