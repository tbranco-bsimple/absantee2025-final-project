using Domain.Models;

namespace Application.DTOs;

public record CreateProjectFromMessageDTO
{
    public Guid Id { get; set; }
    public PeriodDate Period { get; set; }

    public CreateProjectFromMessageDTO(Guid id, PeriodDate period)
    {
        Id = id;
        Period = period;
    }
}