using Application.DTOs;
using Application.Interfaces;
using Domain.Messages;
using Domain.Models;
using InterfaceAdapters.Consumers;
using MassTransit;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class ProjectCreatedConsumerConsumeTests
{
    [Fact]
    public async Task Consume_ShouldCallAddConsumed_WithCorrectData()
    {
        // Arrange
        var serviceDouble = new Mock<IProjectService>();
        var consumer = new ProjectCreatedConsumer(serviceDouble.Object);

        var message = new ProjectCreatedMessage(Guid.NewGuid(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PeriodDate>());


        var context = Mock.Of<ConsumeContext<ProjectCreatedMessage>>(c => c.Message == message);
        var sprintFromMessageDTO = new CreateProjectFromMessageDTO(context.Message.Id, context.Message.PeriodDate);

        // Act
        await consumer.Consume(context);

        // Assert
        serviceDouble.Verify(s => s.AddConsumed(sprintFromMessageDTO), Times.Once);
    }
}