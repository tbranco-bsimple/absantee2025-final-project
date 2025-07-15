using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class UserStoryCreatedConsumer : IConsumer<UserStoryCreatedMessage>
{
    private readonly IUserStoryService _userStoryService;

    public UserStoryCreatedConsumer(IUserStoryService userStoryService)
    {
        _userStoryService = userStoryService;
    }

    public async Task Consume(ConsumeContext<UserStoryCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] UserStoryCreatedConsumer");

        var userStoryDTO = new CreateUserStoryFromMessageDTO(
            context.Message.Id,
            context.Message.Description,
            context.Message.Priority,
            context.Message.Risk
        );
        await _userStoryService.AddConsumed(userStoryDTO);
    }
}
