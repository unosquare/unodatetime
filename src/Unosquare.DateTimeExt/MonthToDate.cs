using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class MonthToDate : YearMonth, IHasWeeks
{
    public MonthToDate(int? month = null, int? year = null)
        : base(month, year)
    {
        Weeks = Enumerable.Range(StartDate.GetWeekOfYear(), EndDate.GetWeekOfYear()).ToArray();
    }

    public MonthToDate(DateTime dateTime)
        : this(dateTime.Month, dateTime.Year)
    {
    }

    public int[] Weeks { get; }

    public new DateTime EndDate => base.EndDate > DateTime.UtcNow.Date ? DateTime.UtcNow.Date : base.EndDate;

    public override string ToString() => $"MTD: {base.ToString()}";
}