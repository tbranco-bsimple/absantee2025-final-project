using Domain.Interfaces;

namespace Domain.Models;

public class UserStory : IUserStory
{
    public Guid Id { get; private set; }

    public UserStory(Guid id)
    {
        Id = id;
    }
}