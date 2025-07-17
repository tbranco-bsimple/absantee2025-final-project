using Application.DTOs;
using Application.Interfaces;
using Application.IPublishers;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class AssociationSprintUserStoryService : IAssociationSprintUserStoryService
{
    private IAssociationSprintUserStoryRepository _associationSprintUserStoryRepository;
    private IAssociationSprintUserStoryFactory _associationSprintUserStoryFactory;
    private readonly IMessagePublisher _publisher;

    public AssociationSprintUserStoryService(IAssociationSprintUserStoryRepository associationSprintUserStoryRepository, IAssociationSprintUserStoryFactory associationSprintUserStoryFactory, IMessagePublisher publisher)
    {
        _associationSprintUserStoryRepository = associationSprintUserStoryRepository;
        _associationSprintUserStoryFactory = associationSprintUserStoryFactory;
        _publisher = publisher;
    }

    public async Task<Result<CreatedAssociationSprintUserStoryDTO>> Create(CreateAssociationSprintUserStoryDTO associationSprintUserStoryDTO)
    {
        IAssociationSprintUserStory newAssociationSprintUserStory;
        try
        {
            newAssociationSprintUserStory = await _associationSprintUserStoryFactory.Create(associationSprintUserStoryDTO.SprintId, associationSprintUserStoryDTO.UserStoryId, associationSprintUserStoryDTO.CollaboratorId, associationSprintUserStoryDTO.EffortHours, associationSprintUserStoryDTO.CompletionPercentage);
            newAssociationSprintUserStory = await _associationSprintUserStoryRepository.AddAsync(newAssociationSprintUserStory);

            var result = new CreatedAssociationSprintUserStoryDTO(newAssociationSprintUserStory.Id, newAssociationSprintUserStory.SprintId, newAssociationSprintUserStory.UserStoryId, newAssociationSprintUserStory.CollaboratorId, newAssociationSprintUserStory.EffortHours, newAssociationSprintUserStory.CompletionPercentage);

            await _publisher.PublishAssociationSprintUserStoryCreatedAsync(newAssociationSprintUserStory);

            return Result<CreatedAssociationSprintUserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<CreatedAssociationSprintUserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task AddConsumed(CreateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO)
    {
        var associationSprintUserStory = await _associationSprintUserStoryRepository.GetByIdAsync(associationSprintUserStoryDTO.Id);

        if (associationSprintUserStory != null)
        {
            Console.WriteLine($"AssociationSprintUserStoryConsumed not added, already exists with Id: {associationSprintUserStoryDTO.Id}");
            return;
        }

        var associationSprintUserStoryVisitor = new AssociationSprintUserStoryDataModel()
        {
            Id = associationSprintUserStoryDTO.Id,
            SprintId = associationSprintUserStoryDTO.SprintId,
            UserStoryId = associationSprintUserStoryDTO.UserStoryId,
            CollaboratorId = associationSprintUserStoryDTO.CollaboratorId,
            EffortHours = associationSprintUserStoryDTO.EffortHours,
            CompletionPercentage = associationSprintUserStoryDTO.CompletionPercentage,
        };

        var newAssociationSprintUserStory = _associationSprintUserStoryFactory.Create(associationSprintUserStoryVisitor);
        await _associationSprintUserStoryRepository.AddAsync(newAssociationSprintUserStory);
    }
}