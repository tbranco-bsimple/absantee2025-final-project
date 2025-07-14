using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

namespace Infrastructure.DataModel;

public class UserStoryDataModel : IUserStoryVisitor
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Risk Risk { get; set; }

    public UserStoryDataModel(IUserStory userStory)
    {
        Id = userStory.Id;
        Description = userStory.Description;
        Priority = userStory.Priority;
        Risk = userStory.Risk;
    }

    public UserStoryDataModel()
    {
    }
}
