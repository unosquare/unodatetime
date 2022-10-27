using System.Collections.ObjectModel;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonth : IYearMonthDateRange, IHasBusinessDays, IHasWeeks, IComparable<YearMonth>
{
    public YearMonth(int? month = null, int? year = null)
    {
        StartDate = new(
            year ?? DateTime.UtcNow.Year,
            month ?? DateTime.UtcNow.Month,
            1);

        Weeks = new(Enumerable.Range(StartDate.GetWeekOfYear(), EndDate.GetWeekOfYear()).ToArray());
    }

    public YearMonth(DateTime dateTime)
        : this(dateTime.Month, dateTime.Year)
    {
    }

    public int Month => StartDate.Month;
    public int Year => StartDate.Year;

    public ReadOnlyCollection<int> Weeks { get; }

    public DateTime StartDate { get; }

    public DateTime EndDate => StartDate.GetLastDayOfMonth();

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();
    public DateTime LastBusinessDay => StartDate.GetLastBusinessDayOfMonth();

    public DateRange DateRange => new(StartDate, EndDate);

    public YearMonth Next => new(StartDate.AddMonths(1));

    public YearMonth Previous => new(StartDate.AddMonths(-1));

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public void Deconstruct(out int year, out int month)
    {
        year = Year;
        month = Month;
    }

    public override string ToString() => $"{Year}-{Month}";

    public int CompareTo(YearMonth? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return ReferenceEquals(null, other) ? 1 : StartDate.CompareTo(other.StartDate);
    }
}