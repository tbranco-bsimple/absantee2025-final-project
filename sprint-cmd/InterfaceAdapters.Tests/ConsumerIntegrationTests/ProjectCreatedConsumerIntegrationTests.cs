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

public class ProjectCreatedConsumerIntegrationTests
{
    [Fact]
    public async Task ShouldConsumeProjectCreatedMessage()
    {
        var services = new ServiceCollection();

        var projectService = new Mock<IProjectService>();

        services.AddSingleton(projectService.Object);

        services.AddMassTransitTestHarness(cfg =>
        {
            cfg.AddConsumer<ProjectCreatedConsumer>();
        });

        await using var provider = services.BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();

        await harness.Start();
        try
        {
            var message = new ProjectCreatedMessage(
                Guid.NewGuid(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<PeriodDate>()
            );

            var projectFromMessageDTO = new CreateProjectFromMessageDTO(message.Id, message.PeriodDate);


            await harness.Bus.Publish(message);

            Assert.True(await harness.Consumed.Any<ProjectCreatedMessage>());

            var consumerHarness = provider.GetRequiredService<IConsumerTestHarness<ProjectCreatedConsumer>>();
            Assert.True(await consumerHarness.Consumed.Any<ProjectCreatedMessage>());

            projectService.Verify(x =>
                x.AddConsumed(projectFromMessageDTO), Times.Once);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
