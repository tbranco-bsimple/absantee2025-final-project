using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.SprintRepositoryTests;

public class SprintRepositoryGetByIdAsyncTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingByExistingId_ThenReturnsSprint()
    {
        // Arrange
        var sprint1 = new Mock<ISprint>();
        var guid1 = Guid.NewGuid();
        var projectId1 = Guid.NewGuid();
        var period1 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));
        var totalEffortHours1 = 1;
        sprint1.Setup(c => c.Id).Returns(guid1);
        sprint1.Setup(c => c.ProjectId).Returns(projectId1);
        sprint1.Setup(c => c.Period).Returns(period1);
        sprint1.Setup(c => c.TotalEffortHours).Returns(totalEffortHours1);
        var sprintDM1 = new SprintDataModel(sprint1.Object);
        context.Sprints.Add(sprintDM1);

        var sprint2 = new Mock<ISprint>();
        var guid2 = Guid.NewGuid();
        var projectId2 = Guid.NewGuid();
        var period2 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(6));
        var totalEffortHours2 = 2;
        sprint2.Setup(c => c.Id).Returns(guid2);
        sprint2.Setup(c => c.ProjectId).Returns(projectId2);
        sprint2.Setup(c => c.Period).Returns(period2);
        sprint2.Setup(c => c.TotalEffortHours).Returns(totalEffortHours2);
        var sprintDM2 = new SprintDataModel(sprint2.Object);
        context.Sprints.Add(sprintDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<ISprint>().Object;

        _mapper.Setup(m => m.Map<SprintDataModel, Sprint>(
            It.Is<SprintDataModel>(t => t.Id == sprintDM1.Id)))
                .Returns(new Sprint(sprintDM1.Id, sprintDM1.ProjectId, sprintDM1.Period, sprintDM1.TotalEffortHours));

        var sprintRepository = new SprintRepository(context, _mapper.Object);

        //Act 
        var result = await sprintRepository.GetByIdAsync(guid1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(sprintDM1.Id, result.Id);
    }

    [Fact]
    public async Task WhenSearchingByNonExistingId_ThenReturnsNull()
    {
        // Arrange
        var sprint1 = new Mock<ISprint>();
        var guid1 = Guid.NewGuid();
        var projectId1 = Guid.NewGuid();
        var period1 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));
        var totalEffortHours1 = 1;
        sprint1.Setup(c => c.Id).Returns(guid1);
        sprint1.Setup(c => c.ProjectId).Returns(projectId1);
        sprint1.Setup(c => c.Period).Returns(period1);
        sprint1.Setup(c => c.TotalEffortHours).Returns(totalEffortHours1);
        var sprintDM1 = new SprintDataModel(sprint1.Object);
        context.Sprints.Add(sprintDM1);

        var sprint2 = new Mock<ISprint>();
        var guid2 = Guid.NewGuid();
        var projectId2 = Guid.NewGuid();
        var period2 = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(6));
        var totalEffortHours2 = 2;
        sprint2.Setup(c => c.Id).Returns(guid2);
        sprint2.Setup(c => c.ProjectId).Returns(projectId2);
        sprint2.Setup(c => c.Period).Returns(period2);
        sprint2.Setup(c => c.TotalEffortHours).Returns(totalEffortHours2);
        var sprintDM2 = new SprintDataModel(sprint2.Object);
        context.Sprints.Add(sprintDM2);

        await context.SaveChangesAsync();

        var expected = new Mock<ISprint>().Object;

        _mapper.Setup(m => m.Map<SprintDataModel, Sprint?>(
            It.IsAny<SprintDataModel>()))
                .Returns((Sprint?)null);

        var sprintRepository = new SprintRepository(context, _mapper.Object);

        //Act 
        var result = await sprintRepository.GetByIdAsync(new Guid());

        //Assert
        Assert.Null(result);
    }

}
