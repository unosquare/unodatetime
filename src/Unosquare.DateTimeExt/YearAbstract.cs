using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class YearAbstract : DateRange, IHasReadOnlyYear, IHasYearWeeks, IHasYearMonths, IHasYearQuarters
{
    protected YearAbstract(DateTime startDate, DateTime endDate)
        : base(startDate, endDate)
    {
        YearWeeks = Weeks.Select(x => new YearWeek(x, Year)).ToArray();
        YearMonths = Months.Select(x => new YearMonth(x, Year)).ToArray();
        YearQuarters = Quarters.Select(x => new YearQuarter(x, Year)).ToArray();
    }

    public int Year => StartDate.Year;

    public YearQuarter Next => new(StartDate.AddYears(1));

    public YearQuarter Previous => new(StartDate.AddYears(-1));

    public bool IsCurrentYear => Year == DateTime.UtcNow.Year;

    public IReadOnlyCollection<YearWeek> YearWeeks { get; }

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public IReadOnlyCollection<YearQuarter> YearQuarters { get; }
}