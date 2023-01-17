﻿using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class YearWeek : DateRange, IYearWeekDateRange, IComparable<YearWeek>
{
    private const int WeekDays = 7;

    public YearWeek(int? week = null, int? year = null)
        : base(GetStartDate(week, year), GetStartDate(week, year).AddDays(6))
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

    public YearWeek(IHasReadOnlyWeek readOnlyWeek, int? year = null)
        : this(readOnlyWeek.Week, year)
    {
    }

    public YearWeek(int week, IHasReadOnlyYear readOnlyYear)
        : this(week, readOnlyYear.Year)
    {
    }

    public YearWeek(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).GetWeekOfYear(), (dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public int Week { get; }
    public int Year => StartDate.Year;

    public YearEntity BoWYearEntity => new(StartDate.Year);
    public YearEntity EoWYearEntity => new(EndDate.Year);

    public YearWeek Next => new(StartDate.AddDays(WeekDays));

    public YearWeek Previous => new(StartDate.AddDays(-WeekDays));

    public bool IsCurrent => Week == DateTime.UtcNow.GetWeekOfYear() && IsCurrentYear;

    public bool IsCurrentYear => Year == DateTime.UtcNow.Year;

    private static DateTime GetStartDate(int? week = null, int? year = null) =>
        DateExtensions.FirstDateOfWeek(year ?? DateTime.UtcNow.Year, week ?? DateTime.UtcNow.GetWeekOfYear());

    public YearWeek AddWeeks(int count) => new(StartDate.AddDays(WeekDays * count));

    public YearWeek ToWeek(int? week) => new(week, Year);

    public void Deconstruct(out int year, out int week)
    {
        year = Year;
        week = Week;
    }

    public override string ToString() => $"{Year}-W{Week}";

    public int CompareTo(YearWeek? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return other is null ? 1 : base.CompareTo(other);
    }
}