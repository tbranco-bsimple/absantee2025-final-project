using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitors;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintFactoryTests
{
    [Fact]
    public async Task Create_WithValidData_ThenSprintIsCreatedCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var sprintPeriod = new PeriodDate(DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today).AddDays(10));
        var totalEffortHours = 2;

        var project = new Mock<IProject>();
        var projectPeriod = new PeriodDate(DateOnly.FromDateTime(DateTime.Today).AddDays(-5), DateOnly.FromDateTime(DateTime.Today).AddDays(15));
        project.Setup(p => p.Period).Returns(projectPeriod);

        var projectRepository = new Mock<IProjectRepository>();
        projectRepository.Setup(p => p.GetByIdAsync(projectId)).ReturnsAsync(project.Object);

        var factory = new SprintFactory(projectRepository.Object);

        // Act
        var result = await factory.Create(projectId, sprintPeriod, totalEffortHours);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectId, result.ProjectId);
        Assert.Equal(sprintPeriod, result.Period);
        Assert.Equal(totalEffortHours, result.TotalEffortHours);
    }

    [Fact]
    public async Task Create_WithInvalidProject_ThenThrowsArgumentException()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var sprintPeriod = new PeriodDate(DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today).AddDays(10));
        var totalEffortHours = 2;

        var projectRepository = new Mock<IProjectRepository>();
        projectRepository.Setup(p => p.GetByIdAsync(projectId)).ReturnsAsync((Project)null);

        var factory = new SprintFactory(projectRepository.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            factory.Create(projectId, sprintPeriod, totalEffortHours));

        Assert.Equal("The project doesn't exist.", exception.Message);
    }

    public static IEnumerable<object[]> InvalidPeriodData =>
        new List<PeriodDate[]>
        {
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 05)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 15)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 25)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 15), new DateOnly(2025, 01, 25)),
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 25), new DateOnly(2025, 01, 30)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 25)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
            new PeriodDate[]
            {
                // sprintPeriod 
                new PeriodDate(new DateOnly(2025, 01, 05), new DateOnly(2025, 01, 20)),
                // projectPeriod
                new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
            },
        };

    [Theory]
    [MemberData(nameof(InvalidPeriodData))]
    public async Task Create_WithInvalidPeriod_ThenThrowsArgumentException(PeriodDate sprintPeriod, PeriodDate projectPeriod)
    {
        // Arrange

        var projectId = Guid.NewGuid();
        var totalEffortHours = 2;

        var project = new Mock<IProject>();
        project.Setup(p => p.Period).Returns(projectPeriod);

        var projectRepository = new Mock<IProjectRepository>();
        projectRepository.Setup(p => p.GetByIdAsync(projectId)).ReturnsAsync(project.Object);

        var factory = new SprintFactory(projectRepository.Object);

        // Act && Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            factory.Create(projectId, sprintPeriod, totalEffortHours));

        Assert.Equal("The sprint's period is outside of project's period.", exception.Message);
    }

    [Fact]
    public void Create_WithVisitor_ReturnsSprintCorrectly()
    {
        // Arrange
        var visitor = new Mock<ISprintVisitor>();
        var sprintId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var period = new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20));
        var totalEffortHours = 40;

        visitor.Setup(v => v.Id).Returns(sprintId);
        visitor.Setup(v => v.ProjectId).Returns(projectId);
        visitor.Setup(v => v.Period).Returns(period);
        visitor.Setup(v => v.TotalEffortHours).Returns(totalEffortHours);

        var projectRepository = new Mock<IProjectRepository>();
        var factory = new SprintFactory(projectRepository.Object);

        // Act
        var result = factory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sprintId, result.Id);
        Assert.Equal(projectId, result.ProjectId);
        Assert.Equal(period, result.Period);
        Assert.Equal(totalEffortHours, result.TotalEffortHours);
    }
}