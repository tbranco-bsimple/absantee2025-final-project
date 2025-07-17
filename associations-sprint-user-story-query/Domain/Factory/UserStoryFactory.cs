using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Domain.Factory;

public class UserStoryFactory : IUserStoryFactory
{
    public UserStoryFactory()
    {
    }

    public IUserStory Create(IUserStoryVisitor visitor)
    {
        return new UserStory(visitor.Id);
    }
}