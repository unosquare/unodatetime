using System.Collections.ObjectModel;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearToDate : DateRange, IHasReadOnlyYear, IHasWeeks, IHasMonths, IHasBusinessDays
{
    public YearToDate(IHasReadOnlyYear readOnlyYear)
        : this(readOnlyYear.Year)
    {
    }

    public YearToDate(int? year = null)
        : base(CalculateStartDate(year).Date, CalculateEndDate(year).Date)
    {
        Months = new(Enumerable.Range(StartDate.Month, EndDate.Month).ToArray());
        Weeks = new(Enumerable.Range(1, EndDate.GetWeekOfYear()).ToArray());
    }

    public int Year => StartDate.Year;

    public ReadOnlyCollection<int> Months { get; }

    public ReadOnlyCollection<int> Weeks { get; }

    public YearQuarter Next => new(StartDate.AddYears(1));

    public YearQuarter Previous => new(StartDate.AddYears(-1));

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();
    public DateTime LastBusinessDay => EndDate.GetLastBusinessDayOfMonth();

    public override string ToString() => $"YTD: {base.ToString()}";

    private static DateTime CalculateEndDate(int? year)
    {
        var endDate = CalculateStartDate(year).AddYears(1).AddDays(-1);

        return endDate > DateTime.UtcNow ? DateTime.UtcNow : endDate;
    }

    private static DateTime CalculateStartDate(int? year) => new(year ?? DateTime.UtcNow.Year, 1, 1);
}