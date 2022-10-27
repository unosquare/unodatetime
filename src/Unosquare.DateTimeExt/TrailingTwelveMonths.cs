using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class TrailingTwelveMonths : DateRange
{
    public TrailingTwelveMonths(DateTime? endDate = null)
        : base((endDate ?? DateTime.UtcNow).AddMonths(-12).Date, (endDate ?? DateTime.UtcNow).Date)
    {
    }

    public TrailingTwelveMonths(IYearMonth yearMonth)
        : this(new DateTime(yearMonth.Year, yearMonth.Month, 1).GetLastDayOfMonth())
    {
    }

    public override string ToString() => $"TTM: {base.ToString()}";
}