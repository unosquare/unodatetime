using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

[DebuggerDisplay("MTD {Year}-{Month} ({ToDateRangeString()})")]
public class MonthToDate : YearMonth
{
    public MonthToDate(int? month = null, int? year = null)
        : base(month, year)
    {
    }

    public MonthToDate(DateTime? dateTime)
        : base(dateTime)
    {
    }

    public MonthToDate(IYearMonth yearMonth)
        : this(yearMonth.Month, yearMonth.Year)
    {
    }

    public MonthToDate(IHasReadOnlyMonth readOnlyMonth, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyMonth.Month, readOnlyYear.Year)
    {
    }

    public new DateTime EndDate => base.EndDate.OrToday();

    public override string ToString() => $"MTD: {base.ToString()}";
}