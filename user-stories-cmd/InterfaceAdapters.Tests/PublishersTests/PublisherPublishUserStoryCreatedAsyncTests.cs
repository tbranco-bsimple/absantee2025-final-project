using Domain.Interfaces;
using Domain.Models;
using MassTransit;
using Moq;
using Domain.Messages;
using InterfaceAdapters.Publishers;

namespace InterfaceAdapters.Tests.PublishersTests;

public class PublisherPublishUserStoryCreatedAsyncTests
{
    [Fact]
    public async Task PublishUserStoryCreated_ShouldPublishEventWithCorrectData()
    {
        // Arrange 
        var publishEndpoint = new Mock<IPublishEndpoint>();

        var publisher = new MassTransitPublisher(publishEndpoint.Object);

        var userStory = new Mock<IUserStory>();
        var id = Guid.NewGuid();
        var description = "description";
        var priority = Priority.Critical;
        var risk = Risk.High;

        userStory.Setup(c => c.Id).Returns(id);
        userStory.Setup(c => c.Description).Returns(description);
        userStory.Setup(c => c.Priority).Returns(priority);
        userStory.Setup(c => c.Risk).Returns(risk);

        // Act 
        await publisher.PublishUserStoryCreatedAsync(userStory.Object);

        // Assert
        publishEndpoint.Verify(
            p => p.Publish(
                It.Is<UserStoryCreatedMessage>(e =>
                    e.Id == id &&
                    e.Description == description &&
                    e.Priority == priority &&
                    e.Risk == risk
                ),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}