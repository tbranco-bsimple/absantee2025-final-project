using Application.DTOs;
using Application.IPublishers;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Application.Tests.SprintServiceTests;

public class SprintServiceCreateTests
{
    [Fact]
    public async Task Create_WithValidData_ReturnsSuccess()
    {
        //Arrange
        var sprintRepository = new Mock<ISprintRepository>();
        var sprintFactory = new Mock<ISprintFactory>();
        var publisher = new Mock<IMessagePublisher>();

        var id = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10));
        var totalEffortHours = 2;

        var sprint = new Sprint(id, projectId, period, totalEffortHours);

        sprintFactory.Setup(x => x.Create(projectId, period, totalEffortHours)).ReturnsAsync(sprint);
        sprintRepository.Setup(x => x.AddAsync(It.IsAny<ISprint>())).ReturnsAsync(sprint);

        var service = new SprintService(sprintRepository.Object, sprintFactory.Object, publisher.Object);

        var createDto = new CreateSprintDTO
        {
            ProjectId = projectId,
            Period = period,
            TotalEffortHours = totalEffortHours
        };

        //Act
        var result = await service.Create(createDto);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(id, result.Value.Id);
        Assert.Equal(projectId, result.Value.ProjectId);
        Assert.Equal(period, result.Value.Period);
        Assert.Equal(totalEffortHours, result.Value.TotalEffortHours);

        publisher.Verify(p => p.PublishSprintCreatedAsync(sprint), Times.Once);
    }

    [Fact]
    public async Task Create_ShouldReturnFailureResult_WhenFactoryThrowsException()
    {
        // Arrange
        var sprintRepository = new Mock<ISprintRepository>();
        var sprintFactory = new Mock<ISprintFactory>();
        var publisher = new Mock<IMessagePublisher>();

        var createDto = new CreateSprintDTO
        {
            ProjectId = Guid.NewGuid(),
            Period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10)),
            TotalEffortHours = 2
        };

        var expectedException = new ArgumentException();

        sprintFactory
            .Setup(f => f.Create(createDto.ProjectId, createDto.Period, createDto.TotalEffortHours))
            .Throws(expectedException);

        var service = new SprintService(sprintRepository.Object, sprintFactory.Object, publisher.Object);

        // Act
        var result = await service.Create(createDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(expectedException.Message, result.Error.Message);

        sprintRepository.Verify(r => r.AddAsync(It.IsAny<ISprint>()), Times.Never);
        publisher.Verify(p => p.PublishSprintCreatedAsync(It.IsAny<ISprint>()), Times.Never);

    }
}
