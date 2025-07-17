using Application.DTOs;
using Application.Interfaces;
using Domain.Factory;
using Domain.IRepository;
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
            Period = sprintDTO.Period,
        };

        var newSprint = _sprintFactory.Create(sprintVisitor);
        await _sprintRepository.AddAsync(newSprint);
    }
}