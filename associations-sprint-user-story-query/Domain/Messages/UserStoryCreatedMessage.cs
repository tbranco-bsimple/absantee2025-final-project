namespace Domain.Messages;

public record UserStoryCreatedMessage(Guid Id, string Description, int Priority, int Risk);
