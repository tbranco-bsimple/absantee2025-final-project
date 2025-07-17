using MassTransit;
using Moq;
using InterfaceAdapters.Publishers;

namespace InterfaceAdapters.Tests.PublishersTests;

public class PublisherConstructorTests
{
    [Fact]
    public void Constructor_ShouldInstantiateWithCorrectData()
    {
        // Arrange 
        var publishEndpoint = new Mock<IPublishEndpoint>();

        // Act 
        new MassTransitPublisher(publishEndpoint.Object);

        // Assert
    }
}