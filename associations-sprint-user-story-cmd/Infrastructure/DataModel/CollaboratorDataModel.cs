using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class CollaboratorDataModel : ICollaboratorVisitor
{
    public Guid Id { get; set; }
    public PeriodDateTime Period { get; set; }

    public CollaboratorDataModel(ICollaborator collaborator)
    {
        Id = collaborator.Id;
        Period = collaborator.Period;
    }

    public CollaboratorDataModel()
    {
    }
}
