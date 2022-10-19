namespace Unosquare.DateTimeExt.Test;

public class DateExtensionsTests
{
    [Fact]
    public void WithDate_ToFormattedString()
    {
        Assert.Equal("June 1, 2022", new DateTime(2022, 6, 1).ToFormattedString());
    }

    [Fact]
    public void WithNullDate_ToFormattedString()
    {
        DateTime? nullDate = null;
        Assert.Equal("", nullDate.ToFormattedString());
    }

    [Fact]
    public void WithNullableDate_ToFormattedString()
    {
        DateTime? nullableDate = new(2022, 6, 1);
        Assert.Equal("June 1, 2022", nullableDate.ToFormattedString());
    }

    [Fact]
    public void WithDate_ToUTC()
    {
        Assert.Equal(new DateTime(2022, 6, 1, 5, 0, 0), new DateTime(2022, 6, 1, 0, 0, 0).ToUtc("Central Standard Time (Mexico)"));
        Assert.Equal(new DateTime(2022, 6, 1, 5, 0, 0), new DateTime(2022, 6, 1, 0, 0, 0).ToUtc("Invalid Timezone Id"));
    }

    [Fact]
    public void WithDate_GetQuarter()
    {
        Assert.Equal(2, new DateTime(2022, 6, 1).GetQuarter());
    }

    [Fact]
    public void WithDate_GetCurrentAndLastQuarter()
    {
        var currentAndLast = new DateTime(2022, 6, 1).GetCurrentAndLastQuarterStartDates();

        Assert.Equal(new DateTime(2022, 4, 1), currentAndLast.CurrentQuarter);
        Assert.Equal(new DateTime(2022, 1, 1), currentAndLast.LastQuarter);
    }

    [Fact]
    public void WithQuarter_GetQuarterDateRange()
    {
        var quarter = 3;
        var range = quarter.GetQuarterDateRange();

        Assert.Equal(new DateTime(2022, 7, 1), range.CurrentQuarter);
        Assert.Equal(new DateTime(2022, 4, 1), range.LastQuarter);
    }

    [Fact]
    public void WithString_ToDateOrNull()
    {
        Assert.Null("not a date format string".ToDateOrNull());
        Assert.Equal(new DateTime(2022, 4, 1), "2022/04/01".ToDateOrNull());
    }

    [Fact]
    public void WithDate_GetWeekOfTheYear()
    {
        Assert.Equal(1, new DateTime(2022, 1, 2).GetWeekOfYear());
    }

    [Fact]
    public void WithYearAndWeek_GetFirstDateOfWeek()
    {
        Assert.Equal(new DateTime(2022, 1, 2), DateExtensions.FirstDateOfWeek(2022, 1));
    }

    [Fact]
    public void WithYearAndMont_GetMontRange()
    {
        var monthRange = 2022.GetMonthRange(10);

        Assert.Equal(new DateTime(2022, 10, 1), monthRange.StartDate);
        Assert.Equal(new DateTime(2022, 10, 31), monthRange.EndDate);
    }

    [Fact]
    public void WithDate_GetFirstDayOfMonth()
    {
        Assert.Equal(new DateTime(2022, 1, 1), new DateTime(2022, 1, 25).GetFirstDayOfMonth());
    }

    [Fact]
    public void WithDate_GetFirstLastOfMonth()
    {
        Assert.Equal(new DateTime(2022, 1, 31), new DateTime(2022, 1, 25).GetLastDayOfMonth());
    }

    [Fact]
    public void WithDate_GetLastBusinessDayOfMonth()
    {
        Assert.Equal(new DateTime(2022, 7, 29), new DateTime(2022, 7, 1).GetLastBusinessDayOfMonth());
    }

    [Fact]
    public void WithStartAndEndDate_GetBusinessDays()
    {
        Assert.Equal(5, new DateTime(2022, 10, 1).GetBusinessDays(new DateTime(2022, 10, 7)));
    }

    [Fact]
    public void WithYearAndWeek_GetMonthOfTheYear()
    {
        Assert.Equal(12, 2022.GetMonthFromWeekYear(52));
    }

    [Fact]
    public void WithDate_IsWeekend()
    {
        Assert.True(new DateTime(2022, 10, 22).IsWeekend());
    }

    [Fact]
    public void WithDate_GetDaysInMonth()
    {
        Assert.Equal(29, new DateTime(2020, 2, 1).GetDaysInMonth());
    }

    [Fact]
    public void WithDate_DiffMonths()
    {
        Assert.Equal(11, new DateTime(2020, 12, 1).DiffMonths(new DateTime(2020, 1, 1)));
    }
}