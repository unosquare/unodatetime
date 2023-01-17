﻿using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonth : DateRange, IYearMonthDateRange, IComparable<YearMonth>
{
    public YearMonth(int? month = null, int? year = null)
        : base(GetStartDate(month, year), GetStartDate(month, year).GetLastDayOfMonth())
    {
    }

    public YearMonth(IYearMonth yearMonth)
        : this(yearMonth.Month, yearMonth.Year)
    {
    }

    public YearMonth(IHasReadOnlyMonth readOnlyMonth, IHasReadOnlyYear readOnlyYear)
        : this(readOnlyMonth.Month, readOnlyYear.Year)
    {
    }

    public YearMonth(IHasReadOnlyMonth readOnlyMonth, int? year = null)
        : this(readOnlyMonth.Month, year)
    {
    }

    public YearMonth(int month, IHasReadOnlyYear readOnlyYear)
        : this(month, readOnlyYear.Year)
    {
    }

    public YearMonth(DateTime dateTime)
        : this(dateTime.Month, dateTime.Year)
    {
    }

    public int Month => StartDate.Month;
    public int Year => StartDate.Year;

    public YearEntity YearEntity => new(Year);

    public YearMonth Next => new(StartDate.AddMonths(1));

    public YearMonth Previous => new(StartDate.AddMonths(-1));

    private static DateTime GetStartDate(int? month = null, int? year = null) => new(
        year ?? DateTime.UtcNow.Year,
        month ?? DateTime.UtcNow.Month,
        1);

    public static bool TryParse(string? value, out YearMonth range)
    {
        range = default!;

        if (value == null)
            return false;

        var values = value.Split('-');

        if (values.Length != 2)
            return false;

        range = new(
            int.TryParse(values[1], out var month)
                ? month
                : throw new ArgumentOutOfRangeException(nameof(value), "Month"),
            int.TryParse(values[0], out var year)
                ? year
                : throw new ArgumentOutOfRangeException(nameof(value), "Year"));

        return true;
    }

    public YearMonth AddMonths(int count) => new(StartDate.AddMonths(count));

    public YearMonth ToMonth(int month) =>
        month is < 1 or > 12
            ? throw new ArgumentOutOfRangeException(nameof(month))
            : new YearMonth(month, Year);

    public void Deconstruct(out int year, out int month)
    {
        year = Year;
        month = Month;
    }

    public override string ToString() => $"{Year}-{Month}";

    public int CompareTo(YearMonth? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }
}