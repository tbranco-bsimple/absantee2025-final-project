using Domain.Models;

namespace Domain.Tests;

public class UserStoryGetPriorityTests
{
    [Fact]
    public void GetPriority_ThenReturnsCorrectly()
    {
        // Arrange
        var priority = Priority.Low;
        UserStory userStory = new UserStory("description", priority, Risk.Critical);

        // Act
        var result = userStory.Priority;

        // Assert
        Assert.Equal(priority, result);
    }
}