namespace Domain.Messages;

public record AssociationSprintUserStoryCreatedMessage(Guid Id, Guid SprintId, Guid UserStoryId, Guid CollaboratorId, int EffortHours, int CompletionPercentage);