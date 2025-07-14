using Domain.Models;

namespace Domain.Tests;

public class UserStoryGetIdTests
{
    [Fact]
    public void GetId_ThenReturnsCorrectly()
    {
        // Arrange
        Guid id = new Guid();
        UserStory userStory = new UserStory(id, "description", Priority.Low, Risk.Critical);

        // Act
        var result = userStory.Id;

        // Assert
        Assert.Equal(id, result);
    }
}