using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

[DebuggerDisplay("Week {Year}-W{Week} ({ToDateRangeString()})")]
public sealed class YearWeek : YearWeekBase, IComparable<YearWeek>
{
    public YearWeek(int? week = null, int? year = null)
        : base(GetStartDate(week, year), GetStartDate(week, year).AddDays(6).ToMidnight())
    {
        Week = StartDate.GetWeekOfYear();
    }

    public YearWeek(IYearWeek yearWeek)
        : this(yearWeek.Week, yearWeek.Year)
    {
    }

    public YearWeek(IHasReadOnlyWeek readOnlyWeek, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyWeek.Week, readOnlyYear.Year)
    {
    }

    public YearWeek(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).GetWeekOfYear(), (dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public static YearWeek Current => new();

    public override int Week { get; }
    public override int Year => StartDate.Year;

    public YearWeek Next => new(StartDate.AddDays(WeekDays));

    public YearWeek Previous => new(StartDate.AddDays(-WeekDays));

    public bool IsCurrent => IsCurrentYear && Week == DateTime.UtcNow.GetWeekOfYear();

    public bool IsCurrentYear => Year == WeekYear;

    private static DateTime GetStartDate(int? week = null, int? year = null) =>
        DateExtensions.FirstDateOfWeek(year ?? WeekYear, week ?? DateTime.UtcNow.GetWeekOfYear());

    public YearWeek AddWeeks(int count) => new(StartDate.AddDays(WeekDays * count));

    public YearWeek ToWeek(int? week) => new(week, Year);

    public int CompareTo(YearWeek? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }

    public static bool TryParse(string? value, out YearWeek result)
    {
        result = null!;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = value.Split('-', 'W');

        if (parts.Length != 3 || !int.TryParse(parts[0], out var year) || !int.TryParse(parts[2], out var week))
        {
            return false;
        }

        result = new(week, year);
        return true;
    }

    private static int WeekYear => DateTime.UtcNow.Month == 1 && DateTime.UtcNow.GetWeekOfYear() > 5
        ? DateTime.UtcNow.Year - 1
        : DateTime.UtcNow.Year;
}