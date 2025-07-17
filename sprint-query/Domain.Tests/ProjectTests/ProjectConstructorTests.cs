using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests;

public class ProjectConstructorTests
{
    [Fact]
    public void Constructor_WithValidData_ThenProjectIsCreatedCorrectly()
    {
        // Arrange

        // Act
        Project project = new Project(Guid.NewGuid(), It.IsAny<PeriodDate>());

        // Assert
        Assert.NotNull(project);
    }
}
