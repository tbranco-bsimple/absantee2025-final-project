using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class SprintFactory : ISprintFactory
{
    private readonly IProjectRepository _projectRepository;

    public SprintFactory(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ISprint> Create(Guid projectId, PeriodDate period, int totalEffortHours)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new ArgumentException("The project doesn't exist.");

        if (!period.IsWithin(project.Period))
            throw new ArgumentException("The sprint's period is outside of project's period.");

        return new Sprint(projectId, period, totalEffortHours);
    }

    public ISprint Create(ISprintVisitor visitor)
    {
        return new Sprint(visitor.Id, visitor.ProjectId, visitor.Period, visitor.TotalEffortHours);
    }
}