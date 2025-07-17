using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class SprintFactory : ISprintFactory
{
    public SprintFactory()
    {
    }
    public ISprint Create(ISprintVisitor visitor)
    {
        return new Sprint(visitor.Id, visitor.Period);
    }
}