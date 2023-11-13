using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class YearAbstract : DateRange, IHasReadOnlyYear, IHasMonths, IHasQuarters, IHasWeeks
{
    protected YearAbstract(DateTime startDate, DateTime endDate)
        : base(startDate, endDate)
    {
        if (StartDate.Year != EndDate.Year)
            throw new ArgumentOutOfRangeException(nameof(startDate), "The year should be the same");

        Months = Enumerable.Range(StartDate.Month, EndDate.Month - StartDate.Month + 1).ToArray();
        Quarters = Enumerable.Range(StartDate.GetQuarter(), EndDate.GetQuarter() - StartDate.GetQuarter() + 1)
            .ToArray();
        Weeks = Enumerable.Range(1, EndDate.GetWeekOfYear()).ToArray();
    }

    public int Year => StartDate.Year;

    public bool IsCurrentYear => Year == DateTime.UtcNow.Year;

    public YearMonth AddMonths(int count) => new(StartDate.AddMonths(count));

    public YearMonth ToMonth(int? month) => new(month, Year);

    public IReadOnlyCollection<int> Weeks { get; }

    public IReadOnlyCollection<int> Months { get; }

    public IReadOnlyCollection<int> Quarters { get; }
}