﻿using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

[DebuggerDisplay("{ToString()} ({ToDateRangeString()})")]
public class YearMonth(int? month = null, int? year = null)
    : YearAbstract<YearMonth>(GetStartDate(month, year), GetStartDate(month, year).GetLastDayOfMonth()),
        IYearMonthDateRange,
        IComparable<YearMonth>
{
    public YearMonth(IYearMonth yearMonth)
        : this(yearMonth.Month, yearMonth.Year)
    {
    }

    public YearMonth(IHasReadOnlyMonth readOnlyMonth, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyMonth.Month, readOnlyYear.Year)
    {
    }

    public YearMonth(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).Month, (dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public YearMonth(IYearWeek yearWeek)
        : this(yearWeek.Year.GetMonthFromWeekYear(yearWeek.Week), yearWeek.Year)
    {
    }

    public static YearMonth Current => new();

    public int Month => StartDate.Month;

    public YearEntity YearEntity => new(Year);

    public override YearMonth Next(int offset = 1) => new(StartDate.AddMonths(offset));

    public override YearMonth Previous(int offset = 1) => new(StartDate.AddMonths(-offset));

    public override bool IsCurrent => Month == DateTime.UtcNow.Month && IsCurrentYear;

    private static DateTime GetStartDate(int? month = null, int? year = null) => new(
        year ?? DateTime.UtcNow.Year,
        month ?? DateTime.UtcNow.Month,
        1);

    public static bool TryParse(string? value, out YearMonth range)
    {
        range = null!;

        if (value == null)
            return false;

        var values = value.Split('-');

        if (values.Length != 2)
            return false;

        try
        {
            range = new(
                int.TryParse(values[1], out var month)
                    ? month
                    : throw new ArgumentOutOfRangeException(nameof(value), "Month"),
                int.TryParse(values[0], out var year)
                    ? year
                    : throw new ArgumentOutOfRangeException(nameof(value), "Year"));

            return true;
        }
        catch
        {
            // Invalid date entered
            return false;
        }
    }

    public DateTime Day(int day) => new(Year, Month, day);

    public DateOnly DayOnly(int day) => new(Year, Month, day);

    public new YearMonthRecord ToRecord() => new() { Year = Year, Month = Month };

    public void Deconstruct(out int year, out int month)
    {
        year = Year;
        month = Month;
    }

    public override string ToString() => $"{Year}-{Month:D2}";

    public int CompareTo(YearMonth? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }
}
