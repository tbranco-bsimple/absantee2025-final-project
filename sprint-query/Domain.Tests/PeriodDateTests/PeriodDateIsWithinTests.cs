using Domain.Models;
using Moq;

namespace Domain.Tests.PeriodDateTests;

public class PeriodDateIsWithinTests
{
    public static IEnumerable<object[]> ValidPeriodData =>
    new List<object[]>
    {
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20)), // inside
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))  // container
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 12), new DateOnly(2025, 01, 18)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 10)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 20), new DateOnly(2025, 01, 20)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 15), new DateOnly(2025, 01, 15)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        }
    };

    [Theory]
    [MemberData(nameof(ValidPeriodData))]
    public void IsWithin_ReturnsTrue_WhenSprintWithinProject(PeriodDate inside, PeriodDate container)
    {
        bool result = inside.IsWithin(container);
        Assert.True(result);
    }

    public static IEnumerable<object[]> InvalidPeriodData =>
    new List<object[]>
    {
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 05)), // inside
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))  // container
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 15)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 25)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 25), new DateOnly(2025, 01, 30)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 25)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        },
        new object[]
        {
            new PeriodDate(new DateOnly(2025, 01, 05), new DateOnly(2025, 01, 20)),
            new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20))
        }
    };


    [Theory]
    [MemberData(nameof(InvalidPeriodData))]
    public void IsWithin_ReturnsFalse_WhenSprintNotWithinProject(PeriodDate inside, PeriodDate container)
    {
        bool result = inside.IsWithin(container);
        Assert.False(result);
    }
}
