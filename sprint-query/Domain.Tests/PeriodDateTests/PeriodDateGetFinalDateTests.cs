using Domain.Models;
using Moq;

namespace Domain.Tests.PeriodDateTests;

public class PeriodDateGetFinalDateTests
{
    [Fact]
    public void GetFinalDate_ThenReturnsCorrectly()
    {
        // Arrange
        var finalDate = new DateOnly();
        var period = new PeriodDate(It.IsAny<DateOnly>(), finalDate);

        // Act
        var result = period.FinalDate;

        // Assert
        Assert.Equal(finalDate, result);
    }
}