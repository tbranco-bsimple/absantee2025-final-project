using Domain.Factory;
using Domain.Models;

namespace Domain.Tests;

public class UserStoryFactoryTests
{

    [Fact]
    public void Create_WithValidData_ThenUserStoryIsCreatedCorrectly()
    {
        // Arrange
        var factory = new UserStoryFactory();

        // Act
        var userStory = factory.Create("description", Priority.Low, Risk.Low);

        // Assert
        Assert.NotNull(userStory);
    }
}
