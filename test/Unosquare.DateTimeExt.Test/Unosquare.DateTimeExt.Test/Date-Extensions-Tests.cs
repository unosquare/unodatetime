namespace Unosquare.DateTimeExt.Test;

public class DateExtensionsTests
{
    public static readonly object[][] BusinessAndWeekendDates =
    {
        new object[] { new DateTime(2022, 10, 3), new DateTime(2022, 10, 8)},
        new object[] { new DateTime(2022, 10, 10), new DateTime(2022, 10, 16)},
        new object[] { new DateTime(2022, 10, 24), new DateTime(2022, 10, 30)}
    };

    public static readonly object[][] MonthsRange =
    {
        new object[] { new DateTime(2022, 01, 01), new DateTime(2022, 12, 01)},
        new object[] { new DateTime(2022, 01, 01), new DateTime(2022, 2, 01)},
        new object[] { new DateTime(2022, 01, 01), new DateTime(2022, 6, 01)}
    };

    public static readonly object[][] NormalAndLeapYear =
    {
        new object[] { new DateTime(2022, 02, 01), new DateTime(2020, 02, 01)},
    };

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
        Assert.Equal(new(2022, 6, 1, 5, 0, 0), new DateTime(2022, 6, 1, 0, 0, 0).ToUtc("Central Standard Time (Mexico)"));
        Assert.Equal(new(2022, 6, 1, 5, 0, 0), new DateTime(2022, 6, 1, 0, 0, 0).ToUtc("Invalid Timezone Id"));
    }

    [Fact]
    public void WithDate_GetQuarter()
    {
        Assert.Equal(2, new DateTime(2022, 6, 1).GetQuarter());
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
        Assert.Equal(new(2022, 1, 2), DateExtensions.FirstDateOfWeek(2022, 1));
    }

    [Fact]
    public void WithDate_GetFirstDayOfMonth()
    {
        Assert.Equal(new(2022, 1, 1), new DateTime(2022, 1, 25).GetFirstDayOfMonth());
    }

    [Fact]
    public void WithDate_GetFirstLastOfMonth()
    {
        Assert.Equal(new(2022, 1, 31), new DateTime(2022, 1, 25).GetLastDayOfMonth());
    }

    [Fact]
    public void WithDate_GetMidnight()
    {
        Assert.Equal(new(2022, 1, 31, 23, 59, 59), new DateTime(2022, 1, 31).ToMidnight());
    }

    [Fact]
    public void WithDate_GetLastBusinessDayOfMonth()
    {
        Assert.Equal(new(2022, 7, 29), new DateTime(2022, 7, 1).GetLastBusinessDayOfMonth());
    }

    [Fact]
    public void WithDate_GetFirstBusinessDayOfMonth()
    {
        Assert.Equal(new(2022, 10, 3), new DateTime(2022, 10, 1).GetFirstBusinessDayOfMonth());
    }
    [Fact]
    public void WithStartAndEndDate_GetBusinessDays()
    {
        Assert.Equal(5, new DateTime(2022, 10, 1).GetBusinessDays(new(2022, 10, 7)));
    }

    [Fact]
    public void WithYearAndWeek_GetMonthOfTheYear()
    {
        Assert.Equal(12, 2022.GetMonthFromWeekYear(52));
    }

    [Theory, MemberData(nameof(BusinessAndWeekendDates))]
    public void WithDate_IsWeekend(DateTime businessDay, DateTime weekendDay)
    {
        Assert.False(businessDay.IsWeekend());
        Assert.True(weekendDay.IsWeekend());
    }

    [Theory, MemberData(nameof(NormalAndLeapYear))]
    public void WithDate_GetDaysInMonth(DateTime normalYear, DateTime leapYear)
    {
        Assert.Equal(28, normalYear.GetDaysInMonth());
        Assert.Equal(29, leapYear.GetDaysInMonth());
    }

    [Theory, MemberData(nameof(MonthsRange))]
    public void WithDate_DiffMonths(DateTime startDate, DateTime endDate)
    {
        Assert.Equal(endDate.Month - startDate.Month, endDate.DiffMonths(startDate));
    }
}