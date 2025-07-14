using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class UserStoryFactory : IUserStoryFactory
{
    public UserStoryFactory()
    {
    }

    public UserStory Create(string description, Priority priority, Risk risk)
    {
        return new UserStory(description, priority, risk);
    }

    public UserStory Create(IUserStoryVisitor visitor)
    {
        return new UserStory(visitor.Id, visitor.Description, visitor.Priority, visitor.Risk);
    }
}