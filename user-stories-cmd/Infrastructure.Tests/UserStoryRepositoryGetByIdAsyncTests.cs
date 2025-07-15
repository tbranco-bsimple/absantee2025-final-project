using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests;

public class UserStoryRepositoryGetByIdAsyncTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingByExistingId_ThenReturnsUserStory()
    {
        // Arrange
        var userStory1 = new Mock<IUserStory>();
        var guid1 = Guid.NewGuid();
        var description1 = "description1";
        var priority1 = Priority.Critical;
        var risk1 = Risk.High;
        userStory1.Setup(c => c.Id).Returns(guid1);
        userStory1.Setup(c => c.Description).Returns(description1);
        userStory1.Setup(c => c.Priority).Returns(priority1);
        userStory1.Setup(c => c.Risk).Returns(risk1);
        var userStoryDM1 = new UserStoryDataModel(userStory1.Object);
        context.UserStories.Add(userStoryDM1);

        var userStory2 = new Mock<IUserStory>();
        var guid2 = Guid.NewGuid();
        var description2 = "description2";
        var priority2 = Priority.Low;
        var risk2 = Risk.Medium;
        userStory2.Setup(c => c.Id).Returns(guid2);
        userStory2.Setup(c => c.Description).Returns(description2);
        userStory2.Setup(c => c.Priority).Returns(priority2);
        userStory2.Setup(c => c.Risk).Returns(risk2);
        var userStoryDM2 = new UserStoryDataModel(userStory2.Object);
        context.UserStories.Add(userStoryDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<IUserStory>().Object;

        _mapper.Setup(m => m.Map<UserStoryDataModel, UserStory>(
            It.Is<UserStoryDataModel>(t => t.Id == userStoryDM1.Id)))
                .Returns(new UserStory(userStoryDM1.Id, userStoryDM1.Description, userStoryDM1.Priority, userStoryDM1.Risk));

        var userStoryRepository = new UserStoryRepository(context, _mapper.Object);

        //Act 
        var result = await userStoryRepository.GetByIdAsync(guid1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(userStoryDM1.Id, result.Id);
    }

    [Fact]
    public async Task WhenSearchingByNonExistingId_ThenReturnsNull()
    {
        // Arrange
        var userStory1 = new Mock<IUserStory>();
        var guid1 = Guid.NewGuid();
        var description1 = "description1";
        var priority1 = Priority.Critical;
        var risk1 = Risk.High;
        userStory1.Setup(c => c.Id).Returns(guid1);
        userStory1.Setup(c => c.Description).Returns(description1);
        userStory1.Setup(c => c.Priority).Returns(priority1);
        userStory1.Setup(c => c.Risk).Returns(risk1);
        var userStoryDM1 = new UserStoryDataModel(userStory1.Object);
        context.UserStories.Add(userStoryDM1);

        var userStory2 = new Mock<IUserStory>();
        var guid2 = Guid.NewGuid();
        var description2 = "description2";
        var priority2 = Priority.Low;
        var risk2 = Risk.Medium;
        userStory2.Setup(c => c.Id).Returns(guid2);
        userStory2.Setup(c => c.Description).Returns(description2);
        userStory2.Setup(c => c.Priority).Returns(priority2);
        userStory2.Setup(c => c.Risk).Returns(risk2);
        var userStoryDM2 = new UserStoryDataModel(userStory2.Object);
        context.UserStories.Add(userStoryDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<IUserStory>().Object;

        _mapper.Setup(m => m.Map<UserStoryDataModel, UserStory?>(
            It.IsAny<UserStoryDataModel>()))
                .Returns((UserStory?)null);

        var userStoryRepository = new UserStoryRepository(context, _mapper.Object);

        //Act 
        var result = await userStoryRepository.GetByIdAsync(new Guid());

        //Assert
        Assert.Null(result);
    }

}
