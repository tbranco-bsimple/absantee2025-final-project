using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class CollaboratorFactory : ICollaboratorFactory
{
    public CollaboratorFactory()
    {
    }
    public ICollaborator Create(ICollaboratorVisitor visitor)
    {
        return new Collaborator(visitor.Id, visitor.Period);
    }
}