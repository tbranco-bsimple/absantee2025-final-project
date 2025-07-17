using Domain.Models;

namespace Application.DTOs;

public record CreateSprintDTO
{
    public Guid ProjectId { get; set; }
    public PeriodDate Period { get; set; }
    public int TotalEffortHours { get; set; }
}