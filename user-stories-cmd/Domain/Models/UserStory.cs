using Domain.Interfaces;

namespace Domain.Models;

public class UserStory : IUserStory
{
    public Guid Id { get; }
    public string Description { get; private set; }
    public Priority Priority { get; private set; }
    public Risk Risk { get; private set; }

    public UserStory(string description, Priority priority, Risk risk)
    {
        ValidateData(description, priority, risk);
        Id = Guid.NewGuid();
        Description = description;
        Priority = priority;
        Risk = risk;
    }

    public UserStory(Guid id, string description, Priority priority, Risk risk)
    {
        ValidateData(description, priority, risk);
        Id = id;
        Description = description;
        Priority = priority;
        Risk = risk;
    }

    public void ValidateData(string description, Priority priority, Risk risk)
    {
        if (string.IsNullOrWhiteSpace(description)
            || description.Length < 10
            || description.Length > 50)
        {
            throw new ArgumentException("Description must be 10-50 alphanumeric characters.", nameof(description));
        }
        if (!Enum.IsDefined(typeof(Priority), priority))
            throw new ArgumentOutOfRangeException(nameof(priority), priority, "Invalid priority value.");

        if (!Enum.IsDefined(typeof(Risk), risk))
            throw new ArgumentOutOfRangeException(nameof(risk), risk, "Invalid risk value.");
    }
}
