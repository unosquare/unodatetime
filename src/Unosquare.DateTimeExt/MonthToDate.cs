namespace Unosquare.DateTimeExt;

public sealed class MonthToDate : YearMonth
{
    public MonthToDate(int? month = null, int? year = null)
        : base(month, year)
    {
    }

    public MonthToDate(DateTime dateTime)
        : base(dateTime)
    {
    }

    public new DateTime EndDate => base.EndDate > DateTime.UtcNow.Date ? DateTime.UtcNow.Date : base.EndDate;

    public override string ToString() => $"MTD: {base.ToString()}";
}