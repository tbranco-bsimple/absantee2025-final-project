using Application.DTOs;
using Application.Interfaces;
using Domain.Messages;
using Domain.Models;
using InterfaceAdapters.Consumers;
using MassTransit;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class SprintCreatedConsumerConsumeTests
{
    [Fact]
    public async Task Consume_ShouldCallAddConsumed_WithCorrectData()
    {
        // Arrange
        var serviceDouble = new Mock<ISprintService>();
        var consumer = new SprintCreatedConsumer(serviceDouble.Object);

        var message = new SprintCreatedMessage(Guid.NewGuid(), Guid.NewGuid(), It.IsAny<PeriodDate>(), It.IsAny<int>());


        var context = Mock.Of<ConsumeContext<SprintCreatedMessage>>(c => c.Message == message);
        var sprintFromMessageDTO = new CreateSprintFromMessageDTO(context.Message.Id, context.Message.ProjectId, context.Message.Period, context.Message.TotalEffortHours);

        // Act
        await consumer.Consume(context);

        // Assert
        serviceDouble.Verify(s => s.AddConsumed(sprintFromMessageDTO), Times.Once);
    }
}