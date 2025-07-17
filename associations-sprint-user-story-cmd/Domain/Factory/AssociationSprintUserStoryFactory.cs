using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class AssociationSprintUserStoryFactory : IAssociationSprintUserStoryFactory
{
    private readonly ISprintRepository _sprintRepository;
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public AssociationSprintUserStoryFactory(ISprintRepository sprintRepository, IUserStoryRepository userStoryRepository, ICollaboratorRepository collaboratorRepository)
    {
        _sprintRepository = sprintRepository;
        _userStoryRepository = userStoryRepository;
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<IAssociationSprintUserStory> Create(Guid sprintId, Guid userStoryId, Guid collaboratorId, int effortHours, int completionPercentage)
    {
        var sprint = await _sprintRepository.GetByIdAsync(sprintId);
        if (sprint == null)
            throw new ArgumentException("The sprint doesn't exist.");

        var userStory = await _userStoryRepository.GetByIdAsync(userStoryId);
        if (userStory == null)
            throw new ArgumentException("The user story doesn't exist.");

        var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);
        if (collaborator == null)
            throw new ArgumentException("The collaborator doesn't exist.");

        if (!sprint.Period.IsWithin(collaborator.Period))
            throw new ArgumentException("The sprint's period is outside of project's period.");

        return new AssociationSprintUserStory(sprintId, userStoryId, collaboratorId, effortHours, completionPercentage);
    }

    public IAssociationSprintUserStory Create(IAssociationSprintUserStoryVisitor visitor)
    {
        return new AssociationSprintUserStory(visitor.Id, visitor.SprintId, visitor.UserStoryId, visitor.CollaboratorId, visitor.EffortHours, visitor.CompletionPercentage);
    }
}