using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

[DebuggerDisplay("ISO Week {Year}-W{Week} ({ToDateRangeString()})")]
public sealed class YearWeekIso(int week, int year) : YearWeekBase(ISOWeek.ToDateTime(year, week, DayOfWeek.Monday),
    ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday).ToMidnight()), IComparable<YearWeekIso>
{
    public YearWeekIso(IYearWeek yearWeek)
        : this(yearWeek.Week, yearWeek.Year)
    {
    }

    public YearWeekIso(IHasReadOnlyWeek readOnlyWeek, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyWeek.Week, readOnlyYear.Year)
    {
    }

    public YearWeekIso(DateTime? dateTime = null)
        : this(ISOWeek.GetWeekOfYear(dateTime ?? DateTime.UtcNow), ISOWeek.GetYear(dateTime ?? DateTime.UtcNow))
    {
    }

    public static YearWeekIso Current => new();

    public override int Week => ISOWeek.GetWeekOfYear(StartDate);
    public override int Year => ISOWeek.GetYear(StartDate);

    public YearWeekIso Next => new(StartDate.AddDays(WeekDays));

    public YearWeekIso Previous => new(StartDate.AddDays(-WeekDays));

    public bool IsCurrent => IsCurrentYear && Week == ISOWeek.GetWeekOfYear(DateTime.UtcNow);

    public bool IsCurrentYear => Year == ISOWeek.GetYear(DateTime.UtcNow);

    public YearWeekIso AddWeeks(int count) => new(StartDate.AddDays(WeekDays * count));

    public YearWeekIso ToWeek(int week) => new(week, Year);

    public int CompareTo(YearWeekIso? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }

    public static bool TryParse(string? value, out YearWeekIso result)
    {
        result = default!;

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
}