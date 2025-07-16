using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests;

public class ProjectGetIdTests
{
    [Fact]
    public void GetId_ThenReturnsCorrectly()
    {
        // Arrange
        var id = new Guid();
        var project = new Project(id, It.IsAny<PeriodDate>());

        // Act
        var result = project.Id;

        // Assert
        Assert.Equal(id, result);
    }
}