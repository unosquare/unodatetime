namespace Unosquare.DateTimeExt.Test;

public class TrailingTwelveMonthsTests
{
    [Fact]
    public void WithTrailingTwelveOnCurrentFirstDayYear_ReturnsAYearAgo()
    {
        Assert.Equal(new(2021, 1, 1), new TrailingTwelveMonths(new DateTime(2022, 1, 1)).StartDate);
    }
}