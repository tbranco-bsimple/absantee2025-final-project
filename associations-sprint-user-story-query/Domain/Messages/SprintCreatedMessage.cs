using Domain.Models;

namespace Domain.Messages;

public record SprintCreatedMessage(Guid Id, Guid ProjectId, PeriodDate Period, int TotalEffortHours);