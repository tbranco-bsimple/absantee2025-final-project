using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class AssociationSprintUserStoryUpdatedConsumer : IConsumer<AssociationSprintUserStoryUpdatedMessage>
{
    private readonly IAssociationSprintUserStoryService _AssociationSprintUserStoryService;

    public AssociationSprintUserStoryUpdatedConsumer(IAssociationSprintUserStoryService AssociationSprintUserStoryService)
    {
        _AssociationSprintUserStoryService = AssociationSprintUserStoryService;
    }

    public async Task Consume(ConsumeContext<AssociationSprintUserStoryUpdatedMessage> context)
    {
        Console.WriteLine("[DEBUG] AssociationSprintUserStoryUpdatedConsumer");

        var AssociationSprintUserStoryDTO = new UpdateAssociationSprintUserStoryFromMessageDTO(
            context.Message.Id,
            context.Message.EffortHours,
            context.Message.CompletionPercentage
        );
        await _AssociationSprintUserStoryService.UpdateConsumed(AssociationSprintUserStoryDTO);
    }
}