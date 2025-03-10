using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class YearWeekBase<T>(DateTime startDate, DateTime endDate)
    : DateRange(startDate, endDate), IYearWeekDateRange, ILinkedEntity<T>
{
    protected const int WeekDays = 7;

    public abstract T Previous(int offset = 1);
    public abstract T Next(int offset = 1);
    public abstract bool IsCurrent { get; }
    public abstract int Year { get; }
    public abstract int Week { get; }

    public YearEntity BoWYearEntity => new(StartDate.Year);
    public YearEntity EoWYearEntity => new(EndDate.Year);

    public new YearWeekRecord ToRecord() => new() { Year = Year, Week = Week };

    public void Deconstruct(out int year, out int week)
    {
        year = Year;
        week = Week;
    }

    public override string ToString() => $"{Year}-W{Week.ToString().PadLeft(2, '0')}";
}
