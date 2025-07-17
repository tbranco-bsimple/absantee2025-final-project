using Domain.Interfaces;

namespace Domain.Models;

public class AssociationSprintUserStory : IAssociationSprintUserStory
{
    public Guid Id { get; set; }
    public Guid UserStoryId { get; set; }
    public Guid SprintId { get; set; }
    public Guid CollaboratorId { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }

    public AssociationSprintUserStory(Guid userStoryId, Guid sprintId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        ValidateCompletionPercentage(completionPercentage);
        Id = Guid.NewGuid();
        UserStoryId = userStoryId;
        SprintId = sprintId;
        CollaboratorId = collaboratorId;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }
    public AssociationSprintUserStory(Guid id, Guid userStoryId, Guid sprintId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        ValidateCompletionPercentage(completionPercentage);
        Id = id;
        UserStoryId = userStoryId;
        SprintId = sprintId;
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
