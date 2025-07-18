using Domain.Interfaces;
using Domain.Models;
using Domain.Visitors;

public interface IUserStoryFactory
{
    IUserStory Create(string description, Priority priority, Risk risk);
    IUserStory Create(IUserStoryVisitor visitor);
}