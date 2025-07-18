using Domain.Models;

namespace Domain.Interfaces;

public interface IUserStory
{
    public Guid Id { get; }
    public string Description { get; }
    public Priority Priority { get; }
    public Risk Risk { get; }
    public void ValidateData(string description, Priority priority, Risk risk);
}