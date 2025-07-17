namespace Application.DTOs;

public record UpdateEffortAndCompletionDTO
{
    public int EffortHours { get; set; }
    public int CompletionPercentage { get; set; }
}