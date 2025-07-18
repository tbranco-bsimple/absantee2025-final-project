
using Domain.Models;

namespace Domain.Visitors;

public interface IUserStoryVisitor
{
    Guid Id { get; }
    string Description { get; }
    Priority Priority { get; }
    Risk Risk { get; }
}

