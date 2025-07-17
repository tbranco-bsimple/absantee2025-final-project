using Application.DTOs;
using Domain.Interfaces;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishAssociationSprintUserStoryCreatedAsync(IAssociationSprintUserStory associationSprintUserStoryDTO);
    Task SendStartSagaCreateSprintMessageAsync(/* StartSagaCreateSagaDTO */ ISprint sprintDTO);
    Task SendCreateSprintCommandAsync(ISprint sprint);
}