using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class ProjectDataModel : IProjectVisitor
{
    public Guid Id { get; set; }
    public PeriodDate Period { get; set; }

    public ProjectDataModel(IProject project)
    {
        Id = project.Id;
        Period = project.Period;
    }

    public ProjectDataModel()
    {
    }
}
