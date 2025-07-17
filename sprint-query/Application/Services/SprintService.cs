using Application.DTOs;
using Application.Interfaces;
using Application.IPublishers;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;

namespace Application.Services;

public class SprintService : ISprintService
{
    private ISprintRepository _sprintRepository;
    private ISprintFactory _sprintFactory;

    public SprintService(ISprintRepository sprintRepository, ISprintFactory sprintFactory)
    {
        _sprintRepository = sprintRepository;
        _sprintFactory = sprintFactory;
    }

    public async Task<Result<IEnumerable<SprintDTO>>> GetAll()
    {
        try
        {
            var sprints = await _sprintRepository.GetAllAsync();
            var result = sprints.Select(s => new SprintDTO(s.Id, s.ProjectId, s.Period, s.TotalEffortHours));

            return Result<IEnumerable<SprintDTO>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IEnumerable<SprintDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<IEnumerable<SprintDTO>>> GetAllByProjectId(Guid projectId)
    {
        try
        {
            var sprints = await _sprintRepository.GetAllByProjectIdAsync(projectId);
            var result = sprints.Select(s => new SprintDTO(s.Id, s.ProjectId, s.Period, s.TotalEffortHours));

            return Result<IEnumerable<SprintDTO>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IEnumerable<SprintDTO>>.Failure(Error.InternalServerError(ex.Message));
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