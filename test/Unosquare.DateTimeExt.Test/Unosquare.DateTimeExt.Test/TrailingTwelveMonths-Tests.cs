namespace Unosquare.DateTimeExt.Test;

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
}