using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class MonthToDate : YearMonth
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

    public MonthToDate(IHasReadOnlyMonth readOnlyMonth, int? year = null)
        : this(readOnlyMonth.Month, year)
    {
    }

    public MonthToDate(int month, IHasReadOnlyYear readOnlyYear)
        : this(month, readOnlyYear.Year)
    {
    }

    public new DateTime EndDate => base.EndDate.OrToday();

    public override string ToString() => $"MTD: {base.ToString()}";
}