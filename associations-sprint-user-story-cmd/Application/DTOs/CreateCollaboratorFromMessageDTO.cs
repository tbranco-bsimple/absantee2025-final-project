using Domain.Models;

namespace Application.DTOs;

public record CreateCollaboratorFromMessageDTO
{
    public Guid Id { get; set; }
    public PeriodDateTime Period { get; set; }

    public CreateCollaboratorFromMessageDTO(Guid id, PeriodDateTime period)
    {
        Id = id;
        Period = period;
    }
}