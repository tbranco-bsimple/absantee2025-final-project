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

    public async Task PublishSprintCreatedAsync(ISprint sprint)
    {
        var eventMessage = new SprintCreatedMessage(
            sprint.Id,
            sprint.ProjectId,
            sprint.Period,
            sprint.TotalEffortHours
        );

        await _publishEndpoint.Publish(eventMessage);

    }
}