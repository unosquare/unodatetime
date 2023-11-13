using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearToDate : YearAbstract, IHasReadOnlyMonth, IHasYearMonths, IHasYearQuarters
{
    public YearToDate(IHasReadOnlyYear readOnlyYear)
        : this(readOnlyYear.Year)
    {
    }

    public YearToDate(int? year = null)
        : base(CalculateStartDate(year).Date, CalculateEndDate(year).Date)
    {
        YearMonths = Months.Select(x => new YearMonth(x, Year)).ToArray();
        YearQuarters = Quarters.Select(x => new YearQuarter(x, Year)).ToArray();
    }

    public static YearToDate Current => new();

    public YearEntity YearEntity => new(Year);

    public int Month => EndDate.Month;

    public MonthToDate MonthToDate => new(StartDate);

    public YearMonth YearMonth => new(StartDate);

    public IReadOnlyCollection<YearMonth> YearMonths { get; }

    public IReadOnlyCollection<YearQuarter> YearQuarters { get; }

    public override string ToString() => $"YTD: {base.ToString()}";

    private static DateTime CalculateEndDate(int? year) => CalculateStartDate(year).AddYears(1).AddDays(-1).OrUtcNow();

    private static DateTime CalculateStartDate(int? year) => new(year ?? DateTime.UtcNow.Year, 1, 1);
}