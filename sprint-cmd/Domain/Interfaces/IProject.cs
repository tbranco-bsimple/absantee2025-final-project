using Domain.Models;

namespace Domain.Interfaces;

public interface IProject
{
    public Guid Id { get; }
    public PeriodDate Period { get; }
}