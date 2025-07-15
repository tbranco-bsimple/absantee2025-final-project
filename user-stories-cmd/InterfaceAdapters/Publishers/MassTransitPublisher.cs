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

    public async Task PublishUserStoryCreatedAsync(IUserStory userStory)
    {
        var eventMessage = new UserStoryCreatedMessage(
            userStory.Id,
            userStory.Description,
            userStory.Priority,
            userStory.Risk
        );

        await _publishEndpoint.Publish(eventMessage);

    }
}