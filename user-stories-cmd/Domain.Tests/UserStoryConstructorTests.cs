using Domain.Models;
using Moq;

namespace Domain.Tests;

public class UserStoryConstructorTests
{
    [Fact]
    public void Constructor_3params_WithValidData_ThenUserStoryIsCreatedCorrectly()
    {
        // Arrange

        // Act
        UserStory userStory = new UserStory("description", Priority.Low, Risk.Critical);

        // Assert
        Assert.NotNull(userStory);
    }

    [Fact]
    public void Constructor_4params_WithValidData_ThenUserStoryIsCreatedCorrectly()
    {
        // Arrange

        // Act
        UserStory userStory = new UserStory(It.IsAny<Guid>(), "description", Priority.Low, Risk.Critical);

        // Assert
        Assert.NotNull(userStory);
    }

    [Theory]
    [InlineData("desc")]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_WithInvalidDescription_ThrowsArgumentException(string description)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var userStory = new UserStory(description, Priority.Low, Risk.High);
        });
    }

    [Theory]
    [InlineData((Priority)0, Risk.Low)]
    [InlineData((Priority)5, Risk.Medium)]
    [InlineData(Priority.High, (Risk)0)]
    [InlineData(Priority.Critical, (Risk)99)]
    public void Constructor_WithInvalidEnumValues_ThrowsArgumentOutOfRangeException(Priority priority, Risk risk)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var userStory = new UserStory("description", priority, risk);
        });
    }
}
