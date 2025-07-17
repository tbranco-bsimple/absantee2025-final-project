using Application.DTOs;
using Domain.Interfaces;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishAssociationSprintUserStoryCreatedAsync(IAssociationSprintUserStory associationSprintUserStory);
    Task PublishAssociationSprintUserStoryUpdatedAsync(IAssociationSprintUserStory associationSprintUserStory);
    Task SendStartSagaCreateSprintMessageAsync(/* StartSagaCreateSagaDTO */ ISprint sprintDTO);
    Task SendCreateSprintCommandAsync(ISprint sprint);
}