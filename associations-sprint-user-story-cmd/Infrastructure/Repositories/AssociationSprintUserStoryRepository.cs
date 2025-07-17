using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssociationSprintUserStoryRepository : IAssociationSprintUserStoryRepository
{
    protected readonly AssociationsSprintUserStoryContext _context;
    private readonly IMapper _mapper;

    public AssociationSprintUserStoryRepository(AssociationsSprintUserStoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IAssociationSprintUserStory> AddAsync(IAssociationSprintUserStory associationSprintUserStory)
    {
        var associationSprintUserStoryDataModel = _mapper.Map<AssociationSprintUserStory, AssociationSprintUserStoryDataModel>((AssociationSprintUserStory)associationSprintUserStory);

        await _context.Set<AssociationSprintUserStoryDataModel>().AddAsync(associationSprintUserStoryDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssociationSprintUserStoryDataModel, AssociationSprintUserStory>(associationSprintUserStoryDataModel);
    }

    public async Task<IAssociationSprintUserStory?> UpdateAsync(IAssociationSprintUserStory associationSprintUserStory)
    {
        var associationSprintUserStoryDataModel = await _context.Set<AssociationSprintUserStoryDataModel>()
            .FirstOrDefaultAsync(a => a.Id == associationSprintUserStory.Id);

        if (associationSprintUserStoryDataModel == null)
            return null;

        associationSprintUserStoryDataModel.EffortHours = associationSprintUserStory.EffortHours;
        associationSprintUserStoryDataModel.CompletionPercentage = associationSprintUserStory.CompletionPercentage;

        _context.Set<AssociationSprintUserStoryDataModel>().Update(associationSprintUserStoryDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssociationSprintUserStoryDataModel, AssociationSprintUserStory>(associationSprintUserStoryDataModel);
    }

    public async Task<IAssociationSprintUserStory?> GetByIdAsync(Guid id)
    {
        var associationSprintUserStoryDataModel = await _context.Set<AssociationSprintUserStoryDataModel>()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (associationSprintUserStoryDataModel == null)
            return null;

        return _mapper.Map<AssociationSprintUserStoryDataModel, AssociationSprintUserStory>(associationSprintUserStoryDataModel);
    }

    public async Task<IAssociationSprintUserStory?> GetBySprintUserStoryAsync(Guid sprintId, Guid userStoryId)
    {
        var associationSprintUserStoryDataModel = await _context.Set<AssociationSprintUserStoryDataModel>()
            .FirstOrDefaultAsync(a => a.SprintId == sprintId &&
                                    a.UserStoryId == userStoryId);

        if (associationSprintUserStoryDataModel == null)
            return null;

        return _mapper.Map<AssociationSprintUserStoryDataModel, AssociationSprintUserStory>(associationSprintUserStoryDataModel);
    }
}