using Domain.Models;

namespace Domain.Messages;

public record UserStoryCreatedMessage(Guid Id, string Description, Priority Priority, Risk Risk);