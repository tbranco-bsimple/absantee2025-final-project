using Domain.Interfaces;
using Domain.Visitors;

namespace Domain.Factory;

public interface ISprintFactory
{
    ISprint Create(ISprintVisitor visitor);
}