using Domain.Models;

namespace Domain.Tests;

public class UserStoryGetRiskTests
{
    [Fact]
    public void GetRisk_ThenReturnsCorrectly()
    {
        // Arrange
        var risk = Risk.Low;
        UserStory userStory = new UserStory("description", Priority.Low, risk);

        // Act
        var result = userStory.Risk;

        // Assert
        Assert.Equal(risk, result);
    }
}