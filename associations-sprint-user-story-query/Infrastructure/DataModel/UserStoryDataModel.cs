using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class UserStoryDataModel : IUserStoryVisitor
{
    public Guid Id { get; set; }

    public UserStoryDataModel(IUserStory userStory)
    {
        Id = userStory.Id;
    }

    public UserStoryDataModel()
    {
    }
}
