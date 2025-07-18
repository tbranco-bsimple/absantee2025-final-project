using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class UserStoryFactory : IUserStoryFactory
{
    public UserStoryFactory()
    {
    }

    public IUserStory Create(string description, Priority priority, Risk risk)
    {
        return new UserStory(description, priority, risk);
    }

    public IUserStory Create(IUserStoryVisitor visitor)
    {
        return new UserStory(visitor.Id, visitor.Description, visitor.Priority, visitor.Risk);
    }
}