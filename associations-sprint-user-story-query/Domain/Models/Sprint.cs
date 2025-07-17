using Domain.Interfaces;

namespace Domain.Models;

public class Sprint : ISprint
{
    public Guid Id { get; }
    public PeriodDate Period { get; private set; }

    public Sprint(PeriodDate period)
    {
        Id = Guid.NewGuid();
        Period = period;
    }

    public Sprint(Guid id, PeriodDate period)
    {
        Id = id;
        Period = period;
    }
}
