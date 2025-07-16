using Domain.Models;
using Moq;

namespace Domain.Tests.SprintTests;

public class SprintGetProjectIdTests
{
    [Fact]
    public void GetProjectId_ThenReturnsCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var sprint = new Sprint(It.IsAny<Guid>(), projectId, It.IsAny<PeriodDate>(), It.IsAny<int>());

        // Act
        var result = sprint.ProjectId;

        // Assert
        Assert.Equal(projectId, result);
    }
}