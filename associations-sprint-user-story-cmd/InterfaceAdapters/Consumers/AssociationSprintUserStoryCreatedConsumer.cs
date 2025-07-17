using Application.Interfaces;
using MassTransit;
using Domain.Messages;
using Application.DTOs;

namespace InterfaceAdapters.Consumers;

public class AssociationSprintUserStoryCreatedConsumer : IConsumer<AssociationSprintUserStoryCreatedMessage>
{
    private readonly IAssociationSprintUserStoryService _AssociationSprintUserStoryService;

    public AssociationSprintUserStoryCreatedConsumer(IAssociationSprintUserStoryService AssociationSprintUserStoryService)
    {
        _AssociationSprintUserStoryService = AssociationSprintUserStoryService;
    }

    public async Task Consume(ConsumeContext<AssociationSprintUserStoryCreatedMessage> context)
    {
        Console.WriteLine("[DEBUG] AssociationSprintUserStoryCreatedConsumer");

        var AssociationSprintUserStoryDTO = new CreateAssociationSprintUserStoryFromMessageDTO(
            context.Message.Id,
            context.Message.SprintId,
            context.Message.UserStoryId,
            context.Message.CollaboratorId,
            context.Message.EffortHours,
            context.Message.CompletionPercentage
        );
        await _AssociationSprintUserStoryService.AddConsumed(AssociationSprintUserStoryDTO);
    }
}