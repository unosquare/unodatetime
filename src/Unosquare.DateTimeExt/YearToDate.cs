using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearToDate : DateRange, IHasReadOnlyYear, IHasWeeks, IHasMonths
{
    public YearToDate(int? year = null)
        : base(CalculateStartDate(year).Date, CalculateEndDate(year).Date)
    {
        Months = Enumerable.Range(StartDate.Month, EndDate.Month).ToArray();
        Weeks = Enumerable.Range(1, EndDate.GetWeekOfYear()).ToArray();
    }

    public int Year => StartDate.Year;

    public int[] Months { get; }

    public int[] Weeks { get; }

    public override string ToString() => $"YTD: {base.ToString()}";

    private static DateTime CalculateEndDate(int? year)
    {
        var startDate = CalculateStartDate(year);
        var endDate = startDate.AddYears(1).AddDays(-1);

        return endDate > DateTime.UtcNow ? DateTime.UtcNow : endDate;
    }

    private static DateTime CalculateStartDate(int? year) => new(year ?? DateTime.UtcNow.Year, 1, 1);
}