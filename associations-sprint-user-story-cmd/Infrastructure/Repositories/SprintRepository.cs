using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SprintRepository : ISprintRepository
{
    protected readonly AssociationsSprintUserStoryContext _context;
    private readonly IMapper _mapper;

    public SprintRepository(AssociationsSprintUserStoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ISprint> AddAsync(ISprint sprint)
    {
        var sprintDataModel = _mapper.Map<Sprint, SprintDataModel>((Sprint)sprint);

        await _context.Set<SprintDataModel>().AddAsync(sprintDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<SprintDataModel, Sprint>(sprintDataModel);
    }

    public async Task<ISprint?> GetByIdAsync(Guid id)
    {
        var sprintDataModel = await _context.Set<SprintDataModel>()
            .FirstOrDefaultAsync(us => us.Id == id);

        if (sprintDataModel == null)
            return null;

        return _mapper.Map<SprintDataModel, Sprint>(sprintDataModel);
    }
}