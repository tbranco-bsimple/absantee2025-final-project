using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public interface IProjectFactory
{
    IProject Create(Guid id, PeriodDate period);
    IProject Create(IProjectVisitor visitor);
}