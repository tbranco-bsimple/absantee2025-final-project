using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests;

public class ProjectGetPeriodTests
{
    [Fact]
    public void GetPeriod_ThenReturnsCorrectly()
    {
        // Arrange
        var period = It.IsAny<PeriodDate>();
        var project = new Project(It.IsAny<Guid>(), period);

        // Act
        var result = project.Period;

        // Assert
        Assert.Equal(period, result);
    }
}