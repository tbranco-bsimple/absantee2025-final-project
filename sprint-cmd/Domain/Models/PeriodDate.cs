namespace Domain.Models;

public class PeriodDate
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public PeriodDate(DateOnly initDate, DateOnly finalDate)
    {
        if (InitDate > FinalDate)
            throw new ArgumentException("Invalid Arguments");

        InitDate = initDate;
        FinalDate = finalDate;
    }

    public bool Contains(PeriodDate periodDate)
    {
        return InitDate <= periodDate.InitDate
        && FinalDate >= periodDate.FinalDate;
    }
}

