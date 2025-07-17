using Application.Interfaces;
using InterfaceAdapters.Consumers;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class SprintCreatedConsumerConstructorTests
{
    [Fact]
    public void Constructor_ShouldInstantiate_WithCorrectData()
    {
        // Arrange
        var serviceMock = new Mock<ISprintService>();

        // Act
        new SprintCreatedConsumer(serviceMock.Object);

        // Assert
    }
}