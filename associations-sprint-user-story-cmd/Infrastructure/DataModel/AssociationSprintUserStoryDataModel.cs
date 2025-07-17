using Domain.Visitors;

namespace Infrastructure.DataModel;

public class AssociationSprintUserStoryDataModel : IAssociationSprintUserStoryVisitor
{
    public Guid Id { get; set; }
    public Guid UserStoryId { get; set; }
    public Guid SprintId { get; set; }
    public Guid CollaboratorId { get; set; }
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }

    public AssociationSprintUserStoryDataModel(Guid id, Guid userStoryId, Guid sprintId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        Id = id;
        UserStoryId = userStoryId;
        SprintId = sprintId;
        CollaboratorId = collaboratorId;
        EffortHours = effortHours;
        CompletionPercentage = completionPercentage;
    }

    public AssociationSprintUserStoryDataModel()
    {
    }
}
