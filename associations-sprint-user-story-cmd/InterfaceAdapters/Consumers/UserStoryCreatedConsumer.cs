using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class UserStoryCreatedConsumer : IConsumer<UserStoryCreatedMessage>
{
    private readonly IUserStoryService _projectService;

    public UserStoryCreatedConsumer(IUserStoryService projectService)
    {
        _projectService = projectService;
    }

    public async Task Consume(ConsumeContext<UserStoryCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] UserStoryCreatedConsumer");

        var projectDTO = new CreateUserStoryFromMessageDTO(
            context.Message.Id
        );
        await _projectService.AddConsumed(projectDTO);
    }
}