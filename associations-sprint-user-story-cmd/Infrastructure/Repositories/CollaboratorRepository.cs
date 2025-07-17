using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    protected readonly AssociationsSprintUserStoryContext _context;
    private readonly IMapper _mapper;

    public CollaboratorRepository(AssociationsSprintUserStoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollaborator> AddAsync(ICollaborator collaborator)
    {
        var collaboratorDataModel = _mapper.Map<Collaborator, CollaboratorDataModel>((Collaborator)collaborator);

        await _context.Set<CollaboratorDataModel>().AddAsync(collaboratorDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<CollaboratorDataModel, Collaborator>(collaboratorDataModel);
    }

    public async Task<ICollaborator?> GetByIdAsync(Guid id)
    {
        var collaboratorDataModel = await _context.Set<CollaboratorDataModel>()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (collaboratorDataModel == null)
            return null;

        return _mapper.Map<CollaboratorDataModel, Collaborator>(collaboratorDataModel);
    }
}