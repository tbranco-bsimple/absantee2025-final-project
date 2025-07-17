using Application.DTOs;
using Application.Interfaces;
using Application.IPublishers;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class SprintService : ISprintService
{
    private ISprintRepository _sprintRepository;
    private ISprintFactory _sprintFactory;
    private readonly IMessagePublisher _publisher;

    public SprintService(ISprintRepository sprintRepository, ISprintFactory sprintFactory, IMessagePublisher publisher)
    {
        _sprintRepository = sprintRepository;
        _sprintFactory = sprintFactory;
        _publisher = publisher;
    }

    public async Task<Result<CreatedSprintDTO>> Create(CreateSprintDTO sprintDTO)
    {
        ISprint newSprint;
        try
        {
            newSprint = await _sprintFactory.Create(sprintDTO.ProjectId, sprintDTO.Period, sprintDTO.TotalEffortHours);
            newSprint = await _sprintRepository.AddAsync(newSprint);

            var result = new CreatedSprintDTO(newSprint.Id, newSprint.ProjectId, newSprint.Period, newSprint.TotalEffortHours);

            await _publisher.PublishSprintCreatedAsync(newSprint);
            return Result<CreatedSprintDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<CreatedSprintDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task AddConsumed(CreateSprintFromMessageDTO sprintDTO)
    {
        var sprint = await _sprintRepository.GetByIdAsync(sprintDTO.Id);

        if (sprint != null)
        {
            Console.WriteLine($"SprintConsumed not added, already exists with Id: {sprintDTO.Id}");
            return;
        }

        var sprintVisitor = new SprintDataModel()
        {
            Id = sprintDTO.Id,
            ProjectId = sprintDTO.ProjectId,
            Period = sprintDTO.Period,
            TotalEffortHours = sprintDTO.TotalEffortHours,
        };

        var newSprint = _sprintFactory.Create(sprintVisitor);
        await _sprintRepository.AddAsync(newSprint);
    }
}