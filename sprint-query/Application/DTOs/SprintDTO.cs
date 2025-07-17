using Domain.Models;

namespace Application.DTOs;

public record SprintDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public PeriodDate Period { get; set; }
    public int TotalEffortHours { get; set; }

    public SprintDTO(Guid id, Guid projectId, PeriodDate period, int totalEffortHours)
    {
        Id = id;
        ProjectId = projectId;
        Period = period;
        TotalEffortHours = totalEffortHours;
    }
}