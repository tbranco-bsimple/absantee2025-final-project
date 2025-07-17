using Domain.Models;
using Moq;

namespace Domain.Tests.PeriodDateTests;

public class PeriodDateConstructorTests
{
    [Fact]
    public void Constructor_WithValidData_ThenPeriodDateIsCreatedCorrectly()
    {
        // Arrange

        // Act
        PeriodDate period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        // Assert
        Assert.NotNull(period);
    }

    public static IEnumerable<object[]> InvalidPeriodDes =>
        new List<object[]>
        {
            new object[] { new DateOnly(2025, 01, 05), new DateOnly(2025, 01, 01) },
            new object[] { new DateOnly(2025, 12, 31), new DateOnly(2025, 01, 01) },
        };

    [Theory]
    [MemberData(nameof(InvalidPeriodDes))]
    public void Constructor_WithInvalidDates_ThenThrowsArgumentException(DateOnly initDate, DateOnly finalDate)
    {
        // Arrange

        // Act && Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new PeriodDate(initDate, finalDate));

        Assert.Equal("Invalid dates.", exception.Message);
    }
}
