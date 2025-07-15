using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class ProjectFactory : IProjectFactory
{
    public ProjectFactory()
    {
    }

    public IProject Create(Guid id, PeriodDate period)
    {
        return new Project(id, period);
    }

    public IProject Create(IProjectVisitor visitor)
    {
        return new Project(visitor.Id, visitor.Period);
    }
}