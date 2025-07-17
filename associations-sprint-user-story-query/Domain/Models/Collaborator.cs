using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public Guid Id { get; }
    public PeriodDateTime Period { get; private set; }

    public Collaborator(PeriodDateTime period)
    {
        Id = Guid.NewGuid();
        Period = period;
    }

    public Collaborator(Guid id, PeriodDateTime period)
    {
        Id = id;
        Period = period;
    }
}
