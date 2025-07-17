using Application.DTOs;
using Application.IPublishers;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Moq;

namespace Application.Tests.SprintServiceTests;

public class SprintServiceAddConsumedTests
{
    [Fact]
    public async Task AddConsumed_WithValidData_ShouldAdd()
    {
        // Arrange
        var sprintRepository = new Mock<ISprintRepository>();
        var sprintFactory = new Mock<ISprintFactory>();

        var id = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10));
        var totalEffortHours = 2;

        var createSprintDTO = new CreateSprintFromMessageDTO(id, projectId, period, totalEffortHours);

        sprintRepository.Setup(r => r.GetByIdAsync(createSprintDTO.Id))
                .ReturnsAsync((Sprint)null);

        var expectedSprint = new Mock<ISprint>();
        sprintFactory.Setup(f => f.Create(It.IsAny<SprintDataModel>()))
               .Returns(expectedSprint.Object);

        var service = new SprintService(sprintRepository.Object, sprintFactory.Object);

        // Act
        await service.AddConsumed(createSprintDTO);

        // Assert
        sprintRepository.Verify(r => r.GetByIdAsync(createSprintDTO.Id), Times.Once);
        sprintFactory.Verify(r => r.Create(It.IsAny<SprintDataModel>()), Times.Once);
        sprintRepository.Verify(r => r.AddAsync(expectedSprint.Object), Times.Once);
    }

    [Fact]
    public async Task AddConsumed_WithAlreadyExistingSprint_ShouldNotAdd()
    {
        // Arrange
        var sprintRepository = new Mock<ISprintRepository>();
        var sprintFactory = new Mock<ISprintFactory>();

        var id = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10));
        var totalEffortHours = 2;

        var createSprintDTO = new CreateSprintFromMessageDTO(id, projectId, period, totalEffortHours);
        var sprint = new Mock<ISprint>();

        sprintRepository.Setup(r => r.GetByIdAsync(createSprintDTO.Id))
                .ReturnsAsync(sprint.Object);

        var service = new SprintService(sprintRepository.Object, sprintFactory.Object);

        // Act
        await service.AddConsumed(createSprintDTO);

        // Assert
        sprintRepository.Verify(r => r.GetByIdAsync(createSprintDTO.Id), Times.Once);
        sprintFactory.Verify(r => r.Create(It.IsAny<SprintDataModel>()), Times.Never);
        sprintRepository.Verify(r => r.AddAsync(It.IsAny<ISprint>()), Times.Never);
    }
}
