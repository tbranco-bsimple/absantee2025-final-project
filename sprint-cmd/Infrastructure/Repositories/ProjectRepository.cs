using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    protected readonly SprintsContext _context;
    private readonly IMapper _mapper;

    public ProjectRepository(SprintsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IProject> AddAsync(IProject project)
    {
        var projectDataModel = _mapper.Map<Project, ProjectDataModel>((Project)project);

        await _context.Set<ProjectDataModel>().AddAsync(projectDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectDataModel, Project>(projectDataModel);
    }

    public async Task<IProject?> GetByIdAsync(Guid id)
    {
        var projectDataModel = await _context.Set<ProjectDataModel>()
            .FirstOrDefaultAsync(us => us.Id == id);

        if (projectDataModel == null)
            return null;

        return _mapper.Map<ProjectDataModel, Project>(projectDataModel);
    }
}