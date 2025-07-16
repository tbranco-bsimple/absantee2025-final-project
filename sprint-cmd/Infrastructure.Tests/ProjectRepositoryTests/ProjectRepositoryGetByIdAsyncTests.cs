using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.ProjectRepositoryTests;

public class ProjectRepositoryGetByIdAsyncTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingByExistingId_ThenReturnsProject()
    {
        // Arrange
        var project1 = new Mock<IProject>();
        var guid1 = Guid.NewGuid();
        var period1 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));
        project1.Setup(c => c.Id).Returns(guid1);
        project1.Setup(c => c.Period).Returns(period1);
        var projectDM1 = new ProjectDataModel(project1.Object);
        context.Projects.Add(projectDM1);

        var project2 = new Mock<IProject>();
        var guid2 = Guid.NewGuid();
        var period2 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(6));
        project2.Setup(c => c.Id).Returns(guid2);
        project2.Setup(c => c.Period).Returns(period2);
        var projectDM2 = new ProjectDataModel(project2.Object);
        context.Projects.Add(projectDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<IProject>().Object;

        _mapper.Setup(m => m.Map<ProjectDataModel, Project>(
            It.Is<ProjectDataModel>(t => t.Id == projectDM1.Id)))
                .Returns(new Project(projectDM1.Id, projectDM1.Period));

        var projectRepository = new ProjectRepository(context, _mapper.Object);

        //Act 
        var result = await projectRepository.GetByIdAsync(guid1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(projectDM1.Id, result.Id);
    }

    [Fact]
    public async Task WhenSearchingByNonExistingId_ThenReturnsNull()
    {
        // Arrange
        var project1 = new Mock<IProject>();
        var guid1 = Guid.NewGuid();
        var period1 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));
        project1.Setup(c => c.Id).Returns(guid1);
        project1.Setup(c => c.Period).Returns(period1);
        var projectDM1 = new ProjectDataModel(project1.Object);
        context.Projects.Add(projectDM1);

        var project2 = new Mock<IProject>();
        var guid2 = Guid.NewGuid();
        var period2 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(6));
        project2.Setup(c => c.Id).Returns(guid2);
        project2.Setup(c => c.Period).Returns(period2);
        var projectDM2 = new ProjectDataModel(project2.Object);
        context.Projects.Add(projectDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<IProject>().Object;

        _mapper.Setup(m => m.Map<ProjectDataModel, Project?>(
            It.IsAny<ProjectDataModel>()))
                .Returns((Project?)null);

        var projectRepository = new ProjectRepository(context, _mapper.Object);

        //Act 
        var result = await projectRepository.GetByIdAsync(new Guid());

        //Assert
        Assert.Null(result);
    }

}
