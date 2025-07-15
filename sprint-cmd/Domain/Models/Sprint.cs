using Domain.Interfaces;

namespace Domain.Models;

public class Sprint : ISprint
{
    public Guid Id { get; }
    public Guid ProjectId { get; }
    public PeriodDate Period { get; private set; }
    public int TotalEffortHours { get; private set; }

    public Sprint(Guid projectId, PeriodDate period, int totalEffortHours)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Period = period;
        TotalEffortHours = totalEffortHours;
    }

    public Sprint(Guid id, Guid projectId, PeriodDate period, int totalEffortHours)
    {
        Id = id;
        ProjectId = projectId;
        Period = period;
        TotalEffortHours = totalEffortHours;
    }
}
