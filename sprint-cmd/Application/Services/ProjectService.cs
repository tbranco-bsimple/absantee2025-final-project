using Application.DTOs;
using Application.Interfaces;
using Domain.Factory;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private IProjectRepository _projectRepository;
    private IProjectFactory _projectFactory;

    public ProjectService(IProjectRepository projectRepository, IProjectFactory projectFactory)
    {
        _projectRepository = projectRepository;
        _projectFactory = projectFactory;
    }

    public async Task AddConsumed(CreateProjectFromMessageDTO projectDTO)
    {
        var project = await _projectRepository.GetByIdAsync(projectDTO.Id);

        if (project != null)
        {
            Console.WriteLine($"ProjectConsumed not added, already exists with Id: {projectDTO.Id}");
            return;
        }

        var projectVisitor = new ProjectDataModel()
        {
            Id = projectDTO.Id,
            Period = projectDTO.Period,
        };

        var newProject = _projectFactory.Create(projectVisitor);
        await _projectRepository.AddAsync(newProject);
    }
}