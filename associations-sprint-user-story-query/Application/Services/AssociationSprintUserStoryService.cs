using Application.DTOs;
using Application.Interfaces;
using Domain.Factory;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class AssociationSprintUserStoryService : IAssociationSprintUserStoryService
{
    private IAssociationSprintUserStoryRepository _associationSprintUserStoryRepository;
    private IAssociationSprintUserStoryFactory _associationSprintUserStoryFactory;

    public AssociationSprintUserStoryService(IAssociationSprintUserStoryRepository associationSprintUserStoryRepository, IAssociationSprintUserStoryFactory associationSprintUserStoryFactory)
    {
        _associationSprintUserStoryRepository = associationSprintUserStoryRepository;
        _associationSprintUserStoryFactory = associationSprintUserStoryFactory;
    }

    public async Task<Result<IEnumerable<AssociationSprintUserStoryDTO>>> GetAll()
    {
        try
        {
            var associationsSprintUserStory = await _associationSprintUserStoryRepository.GetAllAsync();
            var result = associationsSprintUserStory.Select(a => new AssociationSprintUserStoryDTO(a.Id, a.SprintId, a.UserStoryId, a.CollaboratorId, a.EffortHours, a.CompletionPercentage));

            return Result<IEnumerable<AssociationSprintUserStoryDTO>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IEnumerable<AssociationSprintUserStoryDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<IEnumerable<UserStoryDTO>>> GetAllUserStoriesOfSprint(Guid sprintId)
    {
        try
        {
            var associationsSprintUserStory = await _associationSprintUserStoryRepository.GetAllBySprintIdAsync(sprintId);
            var result = associationsSprintUserStory.Select(a => new UserStoryDTO(a.Id));

            return Result<IEnumerable<UserStoryDTO>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IEnumerable<UserStoryDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<UserStoryDTO>> GetUserStoryOfSprint(Guid sprintId, Guid userStoryId)
    {
        try
        {
            var associationsSprintUserStory = await _associationSprintUserStoryRepository.GetBySprintUserStoryAsync(sprintId, userStoryId);
            var result = new UserStoryDTO(associationsSprintUserStory.Id);

            return Result<UserStoryDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<UserStoryDTO>.Failure(Error.InternalServerError(ex.Message));
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