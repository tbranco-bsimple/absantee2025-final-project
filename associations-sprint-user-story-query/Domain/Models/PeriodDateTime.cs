namespace Domain.Models;

public class PeriodDateTime
{
    public DateTime InitDate { get; set; }
    public DateTime FinalDate { get; set; }

    public PeriodDateTime(DateTime initDate, DateTime finalDate)
    {
        if (CheckInputFields(initDate, finalDate))
        {
            InitDate = initDate;
            FinalDate = finalDate;
        }
        else
            throw new ArgumentException("Invalid dates.");
    }

    public PeriodDateTime()
    {

    }

    public PeriodDateTime(PeriodDate periodDate) : this(
        periodDate.InitDate.ToDateTime(TimeOnly.MinValue),
        periodDate.FinalDate.ToDateTime(TimeOnly.MinValue))
    {
    }

    private bool CheckInputFields(DateTime initDate, DateTime finalDate)
    {
        if (initDate > finalDate)
            return false;

        return true;
    }

    public void SetFinalDate(DateTime finalDate)
    {
        FinalDate = finalDate;
    }

    public bool IsFinalDateUndefined()
    {
        return FinalDate == DateTime.MaxValue;
    }

    public bool IsFinalDateSmallerThan(DateTime date)
    {
        return date > FinalDate;
    }

    public bool Contains(PeriodDateTime periodDateTime)
    {
        return InitDate <= periodDateTime.InitDate
            && FinalDate >= periodDateTime.FinalDate;
    }

    public bool Intersects(PeriodDateTime periodDateTime)
    {
        return InitDate <= periodDateTime.FinalDate && periodDateTime.InitDate <= FinalDate;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not PeriodDateTime other)
            return false;

        return InitDate == other.InitDate && FinalDate == other.FinalDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(InitDate, FinalDate);
    }
}

