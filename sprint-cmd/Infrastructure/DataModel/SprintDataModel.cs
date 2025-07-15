using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class SprintDataModel : ISprintVisitor
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public PeriodDate Period { get; set; }
    public int TotalEffortHours { get; set; }

    public SprintDataModel(ISprint sprint)
    {
        Id = sprint.Id;
        ProjectId = sprint.ProjectId;
        Period = sprint.Period;
        TotalEffortHours = sprint.TotalEffortHours;
    }

    public SprintDataModel()
    {
    }
}
