using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearEntity : DateRange, IHasReadOnlyYear, IHasYearWeeks, IHasYearMonths, IHasYearQuarters
{
    public YearEntity(int? year = null)
        : base(new(year ?? DateTime.UtcNow.Year, 1, 1), new(year ?? DateTime.UtcNow.Year, 12, 31))
    {
        YearWeeks = Weeks.Select(x => new YearWeek(x, this)).ToArray();
        YearMonths = Months.Select(x => new YearMonth(x, this)).ToArray();
        YearQuarters = Quarters.Select(x => new YearQuarter(x, this)).ToArray();
    }

    public YearEntity(DateTime dateTime)
        : this(dateTime.Year)
    {
    }

    public int Year => StartDate.Year;

    public IReadOnlyCollection<YearWeek> YearWeeks { get; }

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public IReadOnlyCollection<YearQuarter> YearQuarters { get; }
}