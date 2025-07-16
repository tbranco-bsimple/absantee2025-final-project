using Domain.Models;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintGetIdTests
{
    [Fact]
    public void GetId_ThenReturnsCorrectly()
    {
        // Arrange
        var id = new Guid();
        var sprint = new Sprint(id, It.IsAny<Guid>(), It.IsAny<PeriodDate>(), It.IsAny<int>());

        // Act
        var result = sprint.Id;

        // Assert
        Assert.Equal(id, result);
    }
}