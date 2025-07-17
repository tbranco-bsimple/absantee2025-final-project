using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class CollaboratorCreatedConsumer : IConsumer<CollaboratorCreatedMessage>
{
    private readonly ICollaboratorService _CollaboratorService;

    public CollaboratorCreatedConsumer(ICollaboratorService CollaboratorService)
    {
        _CollaboratorService = CollaboratorService;
    }

    public async Task Consume(ConsumeContext<CollaboratorCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] CollaboratorCreatedConsumer");

        var CollaboratorDTO = new CreateCollaboratorFromMessageDTO(
            context.Message.Id,
            context.Message.PeriodDateTime
        );
        await _CollaboratorService.AddConsumed(CollaboratorDTO);
    }
}