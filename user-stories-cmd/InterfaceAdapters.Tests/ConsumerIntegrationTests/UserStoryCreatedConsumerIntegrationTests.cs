using Application.DTOs;
using Application.Interfaces;
using Domain.Messages;
using Domain.Models;
using InterfaceAdapters.Consumers;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace InterfaceAdapters.Tests.ConsumerIntegrationTests;

public class UserStoryCreatedConsumerIntegrationTests
{
    [Fact]
    public async Task ShouldConsumeUserStoryCreatedMessage()
    {
        var services = new ServiceCollection();

        var collaboratorServiceMock = new Mock<IUserStoryService>();

        services.AddSingleton(collaboratorServiceMock.Object);

        services.AddMassTransitTestHarness(cfg =>
        {
            cfg.AddConsumer<UserStoryCreatedConsumer>();
        });

        await using var provider = services.BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();

        await harness.Start();
        try
        {
            var message = new UserStoryCreatedMessage(
                Guid.NewGuid(),
                "description",
                Priority.High,
                Risk.Critical
            );

            var userStoryFromMessageDTO = new CreateUserStoryFromMessageDTO(message.Id, message.Description, message.Priority, message.Risk);


            await harness.Bus.Publish(message);

            Assert.True(await harness.Consumed.Any<UserStoryCreatedMessage>());

            var consumerHarness = provider.GetRequiredService<IConsumerTestHarness<UserStoryCreatedConsumer>>();
            Assert.True(await consumerHarness.Consumed.Any<UserStoryCreatedMessage>());

            collaboratorServiceMock.Verify(x =>
                x.AddConsumed(userStoryFromMessageDTO), Times.Once);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
