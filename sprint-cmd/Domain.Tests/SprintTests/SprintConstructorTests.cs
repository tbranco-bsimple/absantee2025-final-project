using Domain.Models;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintConstructorTests
{
    [Fact]
    public void Constructor_3params_WithValidData_ThenSprintIsCreatedCorrectly()
    {
        // Arrange

        // Act
        Sprint sprint = new Sprint(Guid.NewGuid(), It.IsAny<PeriodDate>(), It.IsAny<int>());

        // Assert
        Assert.NotNull(sprint);
    }

    [Fact]
    public void Constructor_4params_WithValidData_ThenSprintIsCreatedCorrectly()
    {
        // Arrange

        // Act
        Sprint sprint = new Sprint(Guid.NewGuid(), Guid.NewGuid(), It.IsAny<PeriodDate>(), It.IsAny<int>());

        // Assert
        Assert.NotNull(sprint);
    }
}
