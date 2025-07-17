using Domain.Interfaces;
using Domain.Visitors;

namespace Domain.Factory;

public interface IAssociationSprintUserStoryFactory
{
    IAssociationSprintUserStory Create(IAssociationSprintUserStoryVisitor visitor);
    Task<IAssociationSprintUserStory> Create(Guid sprintId, Guid userStoryId, Guid collaboratorId, int effortHours, int completionPercentage);

}