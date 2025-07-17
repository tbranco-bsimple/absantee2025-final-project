using Domain.Models;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintGetTotalEffortHoursTests
{
    [Fact]
    public void GetTotalEffortHours_ThenReturnsCorrectly()
    {
        // Arrange
        var totalEffortHours = 3;
        var sprint = new Sprint(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<PeriodDate>(), totalEffortHours);

        // Act
        var result = sprint.TotalEffortHours;

        // Assert
        Assert.Equal(totalEffortHours, result);
    }
}