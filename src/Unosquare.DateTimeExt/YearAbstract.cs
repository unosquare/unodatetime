using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class YearAbstract : DateRange, IHasReadOnlyYear, IHasYearWeeks, IHasYearMonths, IHasYearQuarters,
    IHasMonths, IHasQuarters, IHasWeeks
{
    protected YearAbstract(DateTime startDate, DateTime endDate)
        : base(startDate, endDate)
    {
        if (StartDate.Year != endDate.Year)
            throw new ArgumentOutOfRangeException(nameof(startDate), "The year should be same");

        Months = Enumerable.Range(StartDate.Month, EndDate.Month - StartDate.Month + 1).ToArray();
        Quarters = Enumerable.Range(StartDate.GetQuarter(), EndDate.GetQuarter() - StartDate.GetQuarter() + 1)
            .ToArray();
        Weeks = Enumerable.Range(1, EndDate.GetWeekOfYear()).ToArray();

        YearMonths = Months.Select(x => new YearMonth(x, Year)).ToArray();
        YearQuarters = Quarters.Select(x => new YearQuarter(x, Year)).ToArray();
    }

    public int Year => StartDate.Year;

    public YearEntity Next => new(StartDate.AddYears(1));

    public YearEntity Previous => new(StartDate.AddYears(-1));

    public bool IsCurrentYear => Year == DateTime.UtcNow.Year;

    public IReadOnlyCollection<int> Weeks { get; }

    public IReadOnlyCollection<int> Months { get; }

    public IReadOnlyCollection<int> Quarters { get; }

    public IReadOnlyCollection<YearWeek> YearWeeks => throw new NotImplementedException();

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public IReadOnlyCollection<YearQuarter> YearQuarters { get; }
}