using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public interface ISprintFactory
{
    Task<ISprint> Create(Guid projectId, PeriodDate period, int totalEffortHours);
    ISprint Create(ISprintVisitor visitor);
}