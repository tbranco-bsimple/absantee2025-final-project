using Application.DTOs;
using Application.IPublishers;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Moq;

namespace Application.Tests;

public class UserStoryServiceAddConsumedTests
{
    [Fact]
    public async Task AddConsumed_WithValidData_ShouldAdd()
    {
        // Arrange
        var userStoryRepository = new Mock<IUserStoryRepository>();
        var userStoryFactory = new Mock<IUserStoryFactory>();
        var publisher = new Mock<IMessagePublisher>();

        var id = Guid.NewGuid();
        var description = "description";
        var priority = Priority.Critical;
        var risk = Risk.High;

        var createUserStoryDTO = new CreateUserStoryFromMessageDTO(id, description, priority, risk);

        userStoryRepository.Setup(r => r.GetByIdAsync(createUserStoryDTO.Id))
                .ReturnsAsync((UserStory)null);

        var expectedUserStory = new Mock<IUserStory>();
        userStoryFactory.Setup(f => f.Create(It.IsAny<UserStoryDataModel>()))
               .Returns(expectedUserStory.Object);


        var service = new UserStoryService(userStoryRepository.Object, userStoryFactory.Object, publisher.Object);

        // Act
        await service.AddConsumed(createUserStoryDTO);

        // Assert
        userStoryRepository.Verify(r => r.GetByIdAsync(createUserStoryDTO.Id), Times.Once);
        userStoryFactory.Verify(r => r.Create(It.IsAny<UserStoryDataModel>()), Times.Once);
        userStoryRepository.Verify(r => r.AddAsync(expectedUserStory.Object), Times.Once);
    }

    [Fact]
    public async Task AddConsumed_WithAlreadyExistingUserStory_ShouldNotAdd()
    {
        // Arrange
        var userStoryRepository = new Mock<IUserStoryRepository>();
        var userStoryFactory = new Mock<IUserStoryFactory>();
        var publisher = new Mock<IMessagePublisher>();

        var id = Guid.NewGuid();
        var description = "description";
        var priority = Priority.Critical;
        var risk = Risk.High;

        var createUserStoryDTO = new CreateUserStoryFromMessageDTO(id, description, priority, risk);
        var userStory = new Mock<IUserStory>();

        userStoryRepository.Setup(r => r.GetByIdAsync(createUserStoryDTO.Id))
                .ReturnsAsync(userStory.Object);


        var service = new UserStoryService(userStoryRepository.Object, userStoryFactory.Object, publisher.Object);

        // Act
        await service.AddConsumed(createUserStoryDTO);

        // Assert
        userStoryRepository.Verify(r => r.GetByIdAsync(createUserStoryDTO.Id), Times.Once);
        userStoryFactory.Verify(r => r.Create(It.IsAny<UserStoryDataModel>()), Times.Never);
        userStoryRepository.Verify(r => r.AddAsync(It.IsAny<IUserStory>()), Times.Never);
    }
}
