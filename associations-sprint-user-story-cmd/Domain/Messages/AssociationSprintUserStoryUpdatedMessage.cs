namespace Domain.Messages;

public record AssociationSprintUserStoryUpdatedMessage(Guid Id, int EffortHours, int CompletionPercentage);