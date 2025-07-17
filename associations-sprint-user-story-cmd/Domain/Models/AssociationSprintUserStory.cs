using Domain.Interfaces;

namespace Domain.Models;

public class AssociationSprintUserStory : IAssociationSprintUserStory
{
    public Guid Id { get; set; }
    public Guid SprintId { get; set; }
    public Guid UserStoryId { get; set; }
    public Guid CollaboratorId { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }

    public AssociationSprintUserStory(Guid sprintId, Guid userStoryId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        ValidateCompletionPercentage(completionPercentage);
        Id = Guid.NewGuid();
        SprintId = sprintId;
        UserStoryId = userStoryId;
        CollaboratorId = collaboratorId;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }
    public AssociationSprintUserStory(Guid id, Guid sprintId, Guid userStoryId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        ValidateCompletionPercentage(completionPercentage);
        Id = id;
        SprintId = sprintId;
        UserStoryId = userStoryId;
        CollaboratorId = collaboratorId;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }

    public void ValidateCompletionPercentage(int completionPercentage)
    {
        if (completionPercentage > 100)
            throw new ArgumentException("Completion Percentage must be between 0 and 100%.");
    }


}
