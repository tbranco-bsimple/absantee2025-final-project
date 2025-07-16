using Application.DTOs;
using Application.Interfaces;
using Domain.Messages;
using Domain.Models;
using InterfaceAdapters.Consumers;
using MassTransit;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class UserStoryCreatedConsumerConsumeTests
{
    [Fact]
    public async Task Consume_ShouldCallAddConsumed_WithCorrectData()
    {
        // Arrange
        var serviceDouble = new Mock<IUserStoryService>();
        var consumer = new UserStoryCreatedConsumer(serviceDouble.Object);

        var message = new UserStoryCreatedMessage(Guid.NewGuid(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());


        var context = Mock.Of<ConsumeContext<UserStoryCreatedMessage>>(c => c.Message == message);
        var userStoryFromMessageDTO = new CreateUserStoryFromMessageDTO(context.Message.Id, context.Message.Description, (Priority)context.Message.Priority, (Risk)context.Message.Risk);

        // Act
        await consumer.Consume(context);

        // Assert
        serviceDouble.Verify(s => s.AddConsumed(userStoryFromMessageDTO), Times.Once);
    }
}