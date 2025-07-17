
using Domain.Models;

namespace Domain.Visitors;

public interface ISprintVisitor
{
    Guid Id { get; }
    Guid ProjectId { get; }
    PeriodDate Period { get; }
    int TotalEffortHours { get; }
}

