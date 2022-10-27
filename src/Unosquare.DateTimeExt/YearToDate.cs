namespace Unosquare.DateTimeExt;

public class YearToDate : DateRange
{
    public YearToDate(int? year = null) : base(CalculateStartDate(year).Date, CalculateEndDate(year).Date)
    {
    }

    public override string ToString() => $"YTD: {base.ToString()}";

    private static DateTime CalculateEndDate(int? year)
    {
        var startDate = CalculateStartDate(year);
        var endDate = startDate.AddYears(1).AddDays(-1);

        return endDate > DateTime.UtcNow ? DateTime.UtcNow : endDate;
    }

    private static DateTime CalculateStartDate(int? year) => new(year ?? DateTime.UtcNow.Year, 1, 1);
}