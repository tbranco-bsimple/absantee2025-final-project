using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class SprintDataModel : ISprintVisitor
{
    public Guid Id { get; set; }
    public PeriodDate Period { get; set; }

    public SprintDataModel(ISprint sprint)
    {
        Id = sprint.Id;
        Period = sprint.Period;
    }

    public SprintDataModel()
    {
    }
}
