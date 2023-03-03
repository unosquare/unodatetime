using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearToDate : YearAbstract
{
    public YearToDate(IHasReadOnlyYear readOnlyYear)
        : this(readOnlyYear.Year)
    {
    }

    public YearToDate(int? year = null)
        : base(CalculateStartDate(year).Date, CalculateEndDate(year).Date)
    {
    }

    public static YearToDate Current => new();

    public YearEntity YearEntity => new(Year);

    public override string ToString() => $"YTD: {base.ToString()}";

    private static DateTime CalculateEndDate(int? year) => CalculateStartDate(year).AddYears(1).AddDays(-1).OrUtcNow();

    private static DateTime CalculateStartDate(int? year) => new(year ?? DateTime.UtcNow.Year, 1, 1);
}