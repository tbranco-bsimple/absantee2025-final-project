using Domain.Models;

namespace Domain.Tests;

public class UserStoryGetDescriptionTests
{
    [Fact]
    public void GetResult_ThenReturnsCorrectly()
    {
        // Arrange
        var description = "description";
        UserStory userStory = new UserStory(description, Priority.Low, Risk.Critical);

        // Act
        var result = userStory.Description;

        // Assert
        Assert.Equal(description, result);
    }
}