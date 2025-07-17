using Domain.Interfaces;
using Domain.Visitors;

namespace Domain.Factory;

public interface ICollaboratorFactory
{
    ICollaborator Create(ICollaboratorVisitor visitor);
}