using Application.IPublishers;
using Domain.Interfaces;
using Domain.Messages;
using MassTransit;

namespace InterfaceAdapters.Publishers;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAssociationSprintUserStoryCreatedAsync(IAssociationSprintUserStory associationSprintUserStoryDTO)
    {
        var eventMessage = new AssociationSprintUserStoryCreatedMessage(
            associationSprintUserStoryDTO.Id,
            associationSprintUserStoryDTO.SprintId,
            associationSprintUserStoryDTO.UserStoryId,
            associationSprintUserStoryDTO.CollaboratorId,
            associationSprintUserStoryDTO.EffortHours,
            associationSprintUserStoryDTO.CompletionPercentage
        );

        await _publishEndpoint.Publish(eventMessage);
    }

    public async Task PublishAssociationSprintUserStoryUpdatedAsync(IAssociationSprintUserStory associationSprintUserStoryDTO)
    {
        var eventMessage = new AssociationSprintUserStoryUpdatedMessage(
            associationSprintUserStoryDTO.Id,
            associationSprintUserStoryDTO.EffortHours,
            associationSprintUserStoryDTO.CompletionPercentage
        );

        await _publishEndpoint.Publish(eventMessage);
    }

    public Task SendCreateSprintCommandAsync(ISprint sprint)
    {
        throw new NotImplementedException();
    }

    public Task SendStartSagaCreateSprintMessageAsync(ISprint sprintDTO)
    {
        throw new NotImplementedException();
    }
}