using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class TrailingTwelveMonths : DateRange, IHasYearMonths
{
    private const int Twelve = 12;

    public TrailingTwelveMonths(DateTime? endDate = null, int monthsAgo = Twelve)
        : base((endDate ?? DateTime.UtcNow).AddMonths(-monthsAgo).Date, (endDate ?? DateTime.UtcNow).Date)
    {
        var months = new List<YearMonth>();
        var current = new YearMonth(StartDate);

        for (var i = 0; i < monthsAgo; i++)
        {
            months.Add(current);
            current = current.Next;
        }

        YearMonths = months;
    }

    public TrailingTwelveMonths(IYearMonth yearMonth, int monthsAgo = Twelve)
        : this(new DateTime(yearMonth.Year, yearMonth.Month, 1).GetLastDayOfMonth(), monthsAgo)
    {
    }

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public override string ToString() => YearMonths.Count == Twelve
        ? $"TTM: {base.ToString()}"
        : $"Trailing {YearMonths.Count} Months: {base.ToString()}";
}