using Domain.Models;
using Domain.Visitors;

public interface IUserStoryFactory
{
    UserStory Create(string description, Priority priority, Risk risk);
    UserStory Create(IUserStoryVisitor visitor);
}