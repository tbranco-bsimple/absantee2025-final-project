
using Domain.Models;

namespace Domain.Visitors;

public interface IProjectVisitor
{
    Guid Id { get; }
    PeriodDate Period { get; }
}

