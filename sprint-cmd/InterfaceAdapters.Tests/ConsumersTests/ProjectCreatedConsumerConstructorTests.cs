using Application.Interfaces;
using InterfaceAdapters.Consumers;
using Moq;

namespace InterfaceAdapters.Tests.ConsumersTests;

public class ProjectCreatedConsumerConstructorTests
{
    [Fact]
    public void Constructor_ShouldInstantiate_WithCorrectData()
    {
        // Arrange
        var serviceMock = new Mock<IProjectService>();

        // Act
        new ProjectCreatedConsumer(serviceMock.Object);

        // Assert
    }
}