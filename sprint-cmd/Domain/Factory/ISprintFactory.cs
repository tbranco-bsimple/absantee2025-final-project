using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

public interface ISprintFactory
{
    Task<ISprint> Create(Guid projectId, PeriodDate period, int totalEffortHours);
    ISprint Create(ISprintVisitor visitor);
}