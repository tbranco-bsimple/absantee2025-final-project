using Domain.Interfaces;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishUserStoryCreatedAsync(IUserStory userStory);
}