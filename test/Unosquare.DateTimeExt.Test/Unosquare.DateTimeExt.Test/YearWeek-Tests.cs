﻿namespace Unosquare.DateTimeExt.Test;

public class YearWeekTests
{
    [Fact]
    public void WithWeek_Deconstruct()
    {
        var yearMonth = new YearWeek(1, 2022);
        yearMonth.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(2022, 1, 2), startDate);
        Assert.Equal(new(2022, 1, 8), endDate);
    }

    [Fact]
    public void WithWeek_FromDateTime()
    {
        var yearMonth = new YearWeek(new(2022, 1, 2));

        Assert.Equal(1, yearMonth.Week);
        Assert.Equal(2022, yearMonth.Year);
    }

    [Fact]
    public void WithWeek_DateRange()
    {
        var yearMonth = new YearWeek(1, 2022);

        Assert.Equal(new(2022, 1, 2), yearMonth.StartDate);
        Assert.Equal(new(2022, 1, 8), yearMonth.EndDate);
    }

    [Fact]
    public void WithWeek_DeconstructYearMonth()
    {
        var yearMonth = new YearWeek(1, 2022);
        yearMonth.Deconstruct(out int year, out int week);

        Assert.Equal(1, week);
        Assert.Equal(2022, year);
    }
}