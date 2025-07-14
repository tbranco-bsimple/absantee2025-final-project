using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Application.Tests;

public class UserStoryServiceCreateTests
{
    [Fact]
    public async Task Create_WithValidData_ReturnsSuccess()
    {
        //Arrange
        var userStoryRepository = new Mock<IUserStoryRepository>();
        var userStoryFactory = new Mock<IUserStoryFactory>();

        var id = Guid.NewGuid();
        var description = "description";
        var priority = Priority.Critical;
        var risk = Risk.High;

        var userStory = new UserStory(id, description, priority, risk);

        userStoryFactory.Setup(x => x.Create(description, priority, risk)).Returns(userStory);
        userStoryRepository.Setup(x => x.AddAsync(It.IsAny<IUserStory>())).ReturnsAsync(userStory);

        var service = new UserStoryService(userStoryRepository.Object, userStoryFactory.Object);

        var createDto = new CreateUserStoryDTO
        {
            Description = description,
            Priority = priority,
            Risk = risk
        };

        //Act
        var result = await service.Create(createDto);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(id, result.Value.Id);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(priority, result.Value.Priority);
        Assert.Equal(risk, result.Value.Risk);
    }

    [Fact]
    public async Task Create_ShouldReturnFailureResult_WhenFactoryThrowsException()
    {
        // Arrange
        var userStoryRepository = new Mock<IUserStoryRepository>();
        var userStoryFactory = new Mock<IUserStoryFactory>();

        var createDto = new CreateUserStoryDTO
        {
            Description = "invalid qweqweqweqweqweqw",
            Priority = Priority.High,
            Risk = Risk.High
        };

        var expectedException = new ArgumentException();

        userStoryFactory
            .Setup(f => f.Create(createDto.Description, createDto.Priority, createDto.Risk))
            .Throws(expectedException);

        var service = new UserStoryService(userStoryRepository.Object, userStoryFactory.Object);

        // Act
        var result = await service.Create(createDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(expectedException.Message, result.Error.Message);

        userStoryRepository.Verify(r => r.AddAsync(It.IsAny<IUserStory>()), Times.Never);
    }
}
