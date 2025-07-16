using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Moq;

namespace Application.Tests.ProjectServiceTests;

public class ProjectServiceServiceAddConsumedTests
{
    [Fact]
    public async Task AddConsumed_WithValidData_ShouldAdd()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var projectFactory = new Mock<IProjectFactory>();

        var id = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));


        var createProjectDTO = new CreateProjectFromMessageDTO(id, period);

        projectRepository.Setup(r => r.GetByIdAsync(createProjectDTO.Id))
                .ReturnsAsync((Project)null);

        var expectedProject = new Mock<IProject>();
        projectFactory.Setup(f => f.Create(It.IsAny<ProjectDataModel>()))
               .Returns(expectedProject.Object);


        var service = new ProjectService(projectRepository.Object, projectFactory.Object);

        // Act
        await service.AddConsumed(createProjectDTO);

        // Assert
        projectRepository.Verify(r => r.GetByIdAsync(createProjectDTO.Id), Times.Once);
        projectFactory.Verify(r => r.Create(It.IsAny<ProjectDataModel>()), Times.Once);
        projectRepository.Verify(r => r.AddAsync(expectedProject.Object), Times.Once);
    }

    [Fact]
    public async Task AddConsumed_WithAlreadyExistingProject_ShouldNotAdd()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var projectFactory = new Mock<IProjectFactory>();

        var id = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));


        var createProjectDTO = new CreateProjectFromMessageDTO(id, period);
        var project = new Mock<IProject>();

        projectRepository.Setup(r => r.GetByIdAsync(createProjectDTO.Id))
                .ReturnsAsync(project.Object);


        var service = new ProjectService(projectRepository.Object, projectFactory.Object);

        // Act
        await service.AddConsumed(createProjectDTO);

        // Assert
        projectRepository.Verify(r => r.GetByIdAsync(createProjectDTO.Id), Times.Once);
        projectFactory.Verify(r => r.Create(It.IsAny<ProjectDataModel>()), Times.Never);
        projectRepository.Verify(r => r.AddAsync(It.IsAny<IProject>()), Times.Never);
    }
}
