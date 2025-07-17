
using Domain.Models;

namespace Domain.Visitors;

public interface ISprintVisitor
{
    Guid Id { get; }
    PeriodDate Period { get; }
}

