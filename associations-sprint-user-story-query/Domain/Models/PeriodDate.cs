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

    public bool IsWithin(PeriodDateTime container)
    {
        var initDateTime = InitDate.ToDateTime(new TimeOnly(0, 0));
        var finalDateTime = FinalDate.ToDateTime(new TimeOnly(23, 59, 59));

        return container.InitDate <= initDateTime && container.FinalDate >= finalDateTime;
    }
}

