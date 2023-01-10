using System.Collections.ObjectModel;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonth : DateRange, IYearMonthDateRange, IHasBusinessDays, IHasWeeks, IComparable<YearMonth>
{
    public YearMonth(int? month = null, int? year = null)
        : base(GetStartDate(month, year), GetStartDate(month, year).GetLastDayOfMonth())
    {
        Weeks = new(Enumerable.Range(StartDate.GetWeekOfYear(), EndDate.GetWeekOfYear()).ToArray());
    }

    public YearMonth(IYearMonth yearMonth)
        : this(yearMonth.Month, yearMonth.Year)
    {
    }

    public YearMonth(IHasReadOnlyMonth readOnlyMonth, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyMonth.Month, readOnlyYear.Year)
    {
    }

    public YearMonth(IHasReadOnlyMonth readOnlyMonth, int? year = null)
        : this(readOnlyMonth.Month, year)
    {
    }

    public YearMonth(int month, IHasReadOnlyYear readOnlyYear)
        : this(month, readOnlyYear.Year)
    {
    }

    public YearMonth(DateTime dateTime)
        : this(dateTime.Month, dateTime.Year)
    {
    }

    public int Month => StartDate.Month;
    public int Year => StartDate.Year;

    public ReadOnlyCollection<int> Weeks { get; }

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();
    public DateTime LastBusinessDay => StartDate.GetLastBusinessDayOfMonth();

    public YearMonth Next => new(StartDate.AddMonths(1));

    public YearMonth Previous => new(StartDate.AddMonths(-1));

    private static DateTime GetStartDate(int? month = null, int? year = null) => new(
        year ?? DateTime.UtcNow.Year,
        month ?? DateTime.UtcNow.Month,
        1);

    public YearWeek AddMonths(int count) => new(StartDate.AddMonths(count));

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

        return other is null ? 1 : base.CompareTo(other);
    }
}