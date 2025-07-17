using Domain.Interfaces;
using Domain.Visitors;

namespace Domain.Factory;

public interface IUserStoryFactory
{
    IUserStory Create(IUserStoryVisitor visitor);
}