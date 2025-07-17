using Domain.Factory;
using Domain.Models;
using Domain.Visitors;
using Moq;

namespace Domain.Tests.ProjectTests;

public class ProjectFactoryTests
{
    [Fact]
    public void Create_WithVisitor_ReturnsProjectCorrectly()
    {
        // Arrange
        var visitor = new Mock<IProjectVisitor>();
        var id = Guid.NewGuid();
        var period = new PeriodDate(new DateOnly(2025, 01, 10), new DateOnly(2025, 01, 20));

        visitor.Setup(v => v.Id).Returns(id);
        visitor.Setup(v => v.Period).Returns(period);

        var factory = new ProjectFactory();

        // Act
        var result = factory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(period, result.Period);
    }
}