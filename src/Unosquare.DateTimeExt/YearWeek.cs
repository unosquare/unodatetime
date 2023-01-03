using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearWeek : IYearWeekDateRange, IComparable<YearWeek>
{
    private readonly DateTime _startDate;

    public YearWeek(int? week = null, int? year = null)
    {
        _startDate =
            DateExtensions.FirstDateOfWeek(year ?? DateTime.UtcNow.Year, week ?? DateTime.UtcNow.GetWeekOfYear());

        Week = _startDate.GetWeekOfYear();
    }

    public YearWeek(IYearWeek yearWeek)
        : this(yearWeek.Week, yearWeek.Year)
    {
    }

    public YearWeek(IHasReadOnlyWeek readOnlyWeek, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyWeek.Week, readOnlyYear.Year)
    {
    }

    public YearWeek(IHasReadOnlyWeek readOnlyWeek, int? year = null)
        : this(readOnlyWeek.Week, year)
    {
    }

    public YearWeek(int week, IHasReadOnlyYear readOnlyYear)
        : this(week, readOnlyYear.Year)
    {
    }

    public YearWeek(DateTime dateTime)
        : this(dateTime.GetWeekOfYear(), dateTime.Year)
    {
    }

    public int Week { get; }
    public int Year => _startDate.Year;

    public DateTime StartDate => _startDate;
    public DateTime EndDate => _startDate.AddDays(6);

    public DateRange DateRange => new(_startDate, EndDate);

    public YearWeek Next => new(_startDate.AddDays(7));

    public YearWeek Previous => new(_startDate.AddDays(-7));

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public void Deconstruct(out int year, out int week)
    {
        year = Year;
        week = Week;
    }

    public override string ToString() => $"{Year}-W{Week}";

    public int CompareTo(YearWeek? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : _startDate.CompareTo(other._startDate);
    }
}