using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class TrailingTwelveMonths : DateRange
{
    public TrailingTwelveMonths(DateTime? startDate = null)
        : base(startDate ?? DateTime.UtcNow, (startDate ?? DateTime.UtcNow).AddMonths(-12))
    {
    }

    public TrailingTwelveMonths(IYearMonth yearMonth)
        : this(new DateTime(yearMonth.Year, yearMonth.Month, 1).GetLastDayOfMonth())
    {
    }

    public override string ToString() => $"TTM: {base.ToString()}";
}