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

    public async Task<Result<UpdatedAssociationSprintUserStoryDTO>> UpdateEffortCompletion(Guid id, UpdateEffortAndCompletionDTO updateEffortAndCompletionDTO)
    {

        try
        {
            var associationSprintUserStory = await _associationSprintUserStoryRepository.GetByIdAsync(id);
            if (associationSprintUserStory == null)
                return Result<UpdatedAssociationSprintUserStoryDTO>.Failure(Error.NotFound($"Association with ID {id} not found."));

            associationSprintUserStory.UpdateEffortAndCompletion(updateEffortAndCompletionDTO.EffortHours, updateEffortAndCompletionDTO.CompletionPercentage);
            associationSprintUserStory = await _associationSprintUserStoryRepository.UpdateAsync(associationSprintUserStory);

            var result = new UpdatedAssociationSprintUserStoryDTO(associationSprintUserStory.Id, associationSprintUserStory.SprintId, associationSprintUserStory.UserStoryId, associationSprintUserStory.CollaboratorId, associationSprintUserStory.EffortHours, associationSprintUserStory.CompletionPercentage);

            await _publisher.PublishAssociationSprintUserStoryUpdatedAsync(associationSprintUserStory);

            return Result<UpdatedAssociationSprintUserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<UpdatedAssociationSprintUserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task UpdateConsumed(UpdateAssociationSprintUserStoryFromMessageDTO associationSprintUserStoryDTO)
    {
        var existingAssociation = await _associationSprintUserStoryRepository.GetByIdAsync(associationSprintUserStoryDTO.Id);

        if (existingAssociation == null)
        {
            Console.WriteLine($"AssociationSprintUserStoryConsumed not updated, does not exist with Id: {associationSprintUserStoryDTO.Id}");
            return;
        }

        existingAssociation.UpdateEffortAndCompletion(associationSprintUserStoryDTO.EffortHours, associationSprintUserStoryDTO.CompletionPercentage);
        await _associationSprintUserStoryRepository.UpdateAsync(existingAssociation);
    }

}