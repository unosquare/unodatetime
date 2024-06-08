using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

[DebuggerDisplay("{ToString()} ({ToDateRangeString()})")]
public class YearQuarter : DateRange, IYearQuarterDateRange, IComparable<YearQuarter>, IHasMonths
{
    private const int QuarterMonths = 3;

    public YearQuarter(int? quarter = null, int? year = null)
        : base(GetStartDate(quarter, year), GetStartDate(quarter, year).AddMonths(QuarterMonths).AddDays(-1))
    {
        Quarter = StartDate.GetQuarter();

        Months = Enumerable.Range(StartDate.Month, EndDate.Month - StartDate.Month + 1).ToArray();
    }

    public YearQuarter(IYearQuarter yearQuarter)
        : this(yearQuarter.Quarter, yearQuarter.Year)
    {
    }

    public YearQuarter(IHasReadOnlyQuarter readOnlyQuarter, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyQuarter.Quarter, readOnlyYear.Year)
    {
    }

    public YearQuarter(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).GetQuarter(), (dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public static YearQuarter Current => new();

    public int Quarter { get; }

    public int Year => StartDate.Year;

    public IReadOnlyCollection<int> Months { get; }

    public YearEntity YearEntity => new(Year);

    public YearQuarter Next => new(StartDate.AddMonths(QuarterMonths));

    public YearQuarter Previous => new(StartDate.AddMonths(-QuarterMonths));

    public bool IsCurrent => Quarter == DateTime.UtcNow.GetQuarter() && IsCurrentYear;

    public bool IsCurrentYear => Year == DateTime.UtcNow.Year;

    public YearQuarter AddQuarters(int count) => new(StartDate.AddMonths(QuarterMonths * count));

    public YearQuarter ToQuarter(int? quarter) => new(quarter, Year);

    public new YearQuarterRecord ToRecord() => new() { Year = Year, Quarter = Quarter };

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

    public static int MonthInQuarter(int month) => ((month - 1) % QuarterMonths) + 1;

    public static bool TryParse(string? value, out YearQuarter result)
    {
        result = default!;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = value.Split('-', 'Q');

        if (parts.Length != 3 || !int.TryParse(parts[0], out var year) || !int.TryParse(parts[2], out var quarter))
            return false;

        try
        {
            result = new(quarter, year);
            return true;
        }
        catch
        {
            // Invalid date
            return false;
        }
    }

    private static DateTime GetStartDate(int? quarter = null, int? year = null) => new(
        year ?? DateTime.UtcNow.Year,
        ((quarter ?? DateTime.UtcNow.GetQuarter()) - 1) * QuarterMonths + 1,
        1);
}