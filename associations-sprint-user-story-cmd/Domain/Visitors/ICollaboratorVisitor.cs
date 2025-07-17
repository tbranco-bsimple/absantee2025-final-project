
using Domain.Models;

namespace Domain.Visitors;

public interface ICollaboratorVisitor
{
    Guid Id { get; }
    PeriodDateTime Period { get; }
}

