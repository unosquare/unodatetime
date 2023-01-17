using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class TrailingTwelveMonths : DateRange
{
    private const int Twelve = 12;

    private readonly int _monthsAgo;

    public TrailingTwelveMonths(DateTime? endDate = null, int monthsAgo = Twelve)
        : base((endDate ?? DateTime.UtcNow).AddMonths(-monthsAgo).Date, (endDate ?? DateTime.UtcNow).Date)
    {
        _monthsAgo = monthsAgo;
    }

    public TrailingTwelveMonths(IYearMonth yearMonth, int monthsAgo = Twelve)
        : this(new DateTime(yearMonth.Year, yearMonth.Month, 1).GetLastDayOfMonth(), monthsAgo)
    {
    }

    public override string ToString() => _monthsAgo == Twelve
        ? $"TTM: {base.ToString()}"
        : $"Trailing {_monthsAgo} Months: {base.ToString()}";
}