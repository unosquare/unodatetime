using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class TrailingTwelveMonths : DateRange, IHasYearMonths
{
    private const int Twelve = 12;

    public TrailingTwelveMonths(DateTime? endDate = null, int monthsAgo = Twelve)
        : base((endDate ?? DateTime.UtcNow).AddMonths(-monthsAgo).Date, (endDate ?? DateTime.UtcNow).Date)
    {
        YearMonths = Enumerable.Range(0, monthsAgo)
            .Select(i => new YearMonth(StartDate).AddMonths(i))
            .ToList();
    }

    public TrailingTwelveMonths(IYearMonth yearMonth, int monthsAgo = Twelve)
        : this(new DateTime(yearMonth.Year, yearMonth.Month, 1).GetLastDayOfMonth(), monthsAgo)
    {
    }

    public static TrailingTwelveMonths Current => new();

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public override string ToString() => YearMonths.Count == Twelve
        ? $"TTM: {base.ToString()}"
        : $"Trailing {YearMonths.Count} Months: {base.ToString()}";
}