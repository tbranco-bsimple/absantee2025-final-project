using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class ProjectCreatedConsumer : IConsumer<ProjectCreatedMessage>
{
    private readonly IProjectService _projectService;

    public ProjectCreatedConsumer(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task Consume(ConsumeContext<ProjectCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] ProjectCreatedConsumer");

        var projectDTO = new CreateProjectFromMessageDTO(
            context.Message.Id,
            context.Message.PeriodDate
        );
        await _projectService.AddConsumed(projectDTO);
    }
}