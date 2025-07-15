using Application.Interfaces;
using InterfaceAdapters.Consumers;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class UserCreatedConsumerConstructorTests
{
    [Fact]
    public void Constructor_ShouldInstantiate_WithCorrectData()
    {
        // Arrange
        var serviceMock = new Mock<IUserStoryService>();

        // Act
        new UserStoryCreatedConsumer(serviceMock.Object);

        // Assert
    }
}