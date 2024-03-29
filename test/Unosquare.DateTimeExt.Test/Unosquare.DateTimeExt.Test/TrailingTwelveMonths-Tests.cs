﻿namespace Unosquare.DateTimeExt.Test;

public class TrailingTwelveMonthsTests
{
    [Fact]
    public void WithTrailingTwelveOnFirstDayYear_ReturnsAYearAgo()
    {
        Assert.Equal(new(2021, 1, 1), new TrailingTwelveMonths(new DateTime(2022, 1, 1)).StartDate);
    }

    [Fact]
    public void WithTrailingTwelveOnFirstDayYear_ReturnsMonths()
    {
        Assert.Equal(new(2021, 3, 1), new TrailingTwelveMonths(new DateTime(2022, 1, 1)).YearMonths.Skip(2).First().StartDate);
    }

    [Fact]
    public void WithTrailingTwelveOnFirstDayYear_ReturnsFormattedString()
    {
        Assert.Equal("TTM: 1/1/2021 - 1/1/2022", new TrailingTwelveMonths(new DateTime(2022, 1, 1)).ToString());
    }

    [Fact]
    public void TrailingTwelveMonths_Current()
    {
        var ttm = TrailingTwelveMonths.Current;

        Assert.Equal(DateTime.UtcNow.AddMonths(-12).Date, ttm.StartDate);
    }
}