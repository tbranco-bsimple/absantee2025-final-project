using Domain.Models;
using Moq;

namespace Domain.Tests.PeriodDateTests;

public class PeriodDateGetInitDateTests
{
    [Fact]
    public void GetInitDate_ThenReturnsCorrectly()
    {
        // Arrange
        var initDate = new DateOnly();
        var period = new PeriodDate(initDate, It.IsAny<DateOnly>());

        // Act
        var result = period.InitDate;

        // Assert
        Assert.Equal(initDate, result);
    }
}