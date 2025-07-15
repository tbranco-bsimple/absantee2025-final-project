using Domain.Interfaces;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishSprintCreatedAsync(ISprint sprint);
}