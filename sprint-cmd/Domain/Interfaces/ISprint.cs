using Domain.Models;

namespace Domain.Interfaces;

public interface ISprint
{
    public Guid Id { get; }
    public Guid ProjectId { get; }
    public PeriodDate Period { get; }
    public int TotalEffortHours { get; }
}