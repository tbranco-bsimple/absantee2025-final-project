using Domain.Models;

namespace Domain.Interfaces;

public interface ISprint
{
    public Guid Id { get; }
    public PeriodDate Period { get; }
}