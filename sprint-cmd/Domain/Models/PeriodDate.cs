namespace Domain.Models;

public class PeriodDate
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public PeriodDate(DateOnly initDate, DateOnly finalDate)
    {
        if (initDate > finalDate)
            throw new ArgumentException("Invalid dates.");

        InitDate = initDate;
        FinalDate = finalDate;
    }

    public bool IsWithin(PeriodDate container)
    {
        return container.InitDate <= InitDate && container.FinalDate >= FinalDate;
    }
}

