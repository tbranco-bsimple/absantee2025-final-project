using Domain.Interfaces;
using Domain.Models;
using MassTransit;
using Moq;
using Domain.Messages;
using InterfaceAdapters.Publishers;

namespace InterfaceAdapters.Tests.PublishersTests;

public class PublisherPublishSprintCreatedAsyncTests
{
    [Fact]
    public async Task PublishSprintCreated_ShouldPublishEventWithCorrectData()
    {
        // Arrange 
        var publishEndpoint = new Mock<IPublishEndpoint>();

        var publisher = new MassTransitPublisher(publishEndpoint.Object);

        var sprint = new Mock<ISprint>();
        var id = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10));
        var totalEffortHours = 2;

        sprint.Setup(c => c.Id).Returns(id);
        sprint.Setup(c => c.ProjectId).Returns(projectId);
        sprint.Setup(c => c.Period).Returns(period);
        sprint.Setup(c => c.TotalEffortHours).Returns(totalEffortHours);

        // Act 
        await publisher.PublishSprintCreatedAsync(sprint.Object);

        // Assert
        publishEndpoint.Verify(
            p => p.Publish(
                It.Is<SprintCreatedMessage>(e =>
                    e.Id == id &&
                    e.ProjectId == projectId &&
                    e.Period == period &&
                    e.TotalEffortHours == totalEffortHours
                ),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}