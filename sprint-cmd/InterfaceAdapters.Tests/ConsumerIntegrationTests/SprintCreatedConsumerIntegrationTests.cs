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

public class SprintCreatedConsumerIntegrationTests
{
    [Fact]
    public async Task ShouldConsumeSprintCreatedMessage()
    {
        var services = new ServiceCollection();

        var sprintService = new Mock<ISprintService>();

        services.AddSingleton(sprintService.Object);

        services.AddMassTransitTestHarness(cfg =>
        {
            cfg.AddConsumer<SprintCreatedConsumer>();
        });

        await using var provider = services.BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();

        await harness.Start();
        try
        {
            var message = new SprintCreatedMessage(
                Guid.NewGuid(),
                Guid.NewGuid(),
                It.IsAny<PeriodDate>(),
                It.IsAny<int>()

            );

            var sprintFromMessageDTO = new CreateSprintFromMessageDTO(message.Id, message.ProjectId, message.Period, message.TotalEffortHours);


            await harness.Bus.Publish(message);

            Assert.True(await harness.Consumed.Any<SprintCreatedMessage>());

            var consumerHarness = provider.GetRequiredService<IConsumerTestHarness<SprintCreatedConsumer>>();
            Assert.True(await consumerHarness.Consumed.Any<SprintCreatedMessage>());

            sprintService.Verify(x =>
                x.AddConsumed(sprintFromMessageDTO), Times.Once);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
