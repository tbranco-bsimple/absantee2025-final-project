using Domain.Models;

namespace Application.DTOs;

public record CreateSprintFromMessageDTO
{
    public Guid Id { get; set; }
    public PeriodDate Period { get; set; }

    public CreateSprintFromMessageDTO(Guid id, PeriodDate period)
    {
        Id = id;
        Period = period;
    }
}