using Domain.Models;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintGetPeriodTests
{
    [Fact]
    public void GetPeriod_ThenReturnsCorrectly()
    {
        // Arrange
        var period = It.IsAny<PeriodDate>();
        var sprint = new Sprint(It.IsAny<Guid>(), It.IsAny<Guid>(), period, It.IsAny<int>());

        // Act
        var result = sprint.Period;

        // Assert
        Assert.Equal(period, result);
    }
}