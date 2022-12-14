using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearQuarter : DateRange, IYearQuarterDateRange, IComparable<YearQuarter>
{
    private const int QuarterMonths = 3;

    public YearQuarter(int? quarter = null, int? year = null)
        : base(GetStartDate(quarter, year), GetStartDate(quarter, year).AddMonths(QuarterMonths).AddDays(-1))
    {
    }

    public YearQuarter(IYearQuarter yearQuarter)
        : this(yearQuarter.Quarter, yearQuarter.Year)
    {
    }

    public YearQuarter(IHasReadOnlyQuarter readOnlyQuarter, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyQuarter.Quarter, readOnlyYear.Year)
    {
    }

    public YearQuarter(IHasReadOnlyQuarter readOnlyQuarter, int? year = null)
        : this(readOnlyQuarter.Quarter, year)
    {
    }

    public YearQuarter(int quarter, IHasReadOnlyYear readOnlyYear)
        : this(quarter, readOnlyYear.Year)
    {
    }

    public YearQuarter(DateTime dateTime)
        : this(dateTime.GetQuarter(), dateTime.Year)
    {

    }

    public int Quarter => StartDate.GetQuarter();
    public int Year => StartDate.Year;

    public YearQuarter Next => new(StartDate.AddMonths(QuarterMonths));

    public YearQuarter Previous => new(StartDate.AddMonths(-QuarterMonths));

    private static DateTime GetStartDate(int? quarter = null, int? year = null) => new(
        year ?? DateTime.UtcNow.Year,
        ((quarter ?? DateTime.UtcNow.GetQuarter()) - 1) * QuarterMonths + 1,
        1);

    public YearWeek AddQuarters(int count) => new(StartDate.AddMonths(QuarterMonths * count));

    public void Deconstruct(out int year, out int quarter)
    {
        year = Year;
        quarter = Quarter;
    }

    public override string ToString() => $"{Year}-Q{Quarter}";

    public int CompareTo(YearQuarter? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }
}