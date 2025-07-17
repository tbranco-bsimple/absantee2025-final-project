using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class SprintCreatedConsumer : IConsumer<SprintCreatedMessage>
{
    private readonly ISprintService _sprintService;

    public SprintCreatedConsumer(ISprintService sprintService)
    {
        _sprintService = sprintService;
    }

    public async Task Consume(ConsumeContext<SprintCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] SprintCreatedConsumer");

        var sprintDTO = new CreateSprintFromMessageDTO(
            context.Message.Id,
            context.Message.Period
        );
        await _sprintService.AddConsumed(sprintDTO);
    }
}