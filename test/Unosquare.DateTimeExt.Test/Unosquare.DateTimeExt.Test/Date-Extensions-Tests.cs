namespace Unosquare.DateTimeExt.Test;

public class DateExtensionsTests
{
    public static readonly object[][] BusinessAndWeekendDates =
    [
        [new DateTime(2022, 10, 3), new DateTime(2022, 10, 8)],
        [new DateTime(2022, 10, 10), new DateTime(2022, 10, 16)],
        [new DateTime(2022, 10, 24), new DateTime(2022, 10, 30)],
    ];

    public static readonly object[][] MonthsRange =
    [
        [new DateTime(2022, 01, 01), new DateTime(2022, 12, 01)],
        [new DateTime(2022, 01, 01), new DateTime(2022, 2, 01)],
        [new DateTime(2022, 01, 01), new DateTime(2022, 6, 01)],
    ];

    public static readonly object[][] NormalAndLeapYear =
    [
        [new DateTime(2022, 02, 01), new DateTime(2020, 02, 01)],
    ];

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
        Assert.Equal(new(2022, 6, 1, 4, 0, 0), new DateTime(2022, 6, 1, 0, 0, 0).ToUtc("Invalid Timezone Id"));
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
        Assert.Equal(new(2024, 1, 7), DateExtensions.FirstDateOfWeek(2024, 1));
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
    [Fact]
    public void ToFormattedString_ReturnsFormattedString()
    {
        var date = new DateTime(2022, 1, 1);
        var formattedString = date.ToFormattedString();

        Assert.Equal("January 1, 2022", formattedString);
    }

    [Fact]
    public void ToFormattedString_Nullable_ReturnsFormattedString()
    {
        DateTime? date = new DateTime(2022, 1, 1);
        var formattedString = date.ToFormattedString();

        Assert.Equal("January 1, 2022", formattedString);
    }

    [Fact]
    public void ToFormattedString_Null_ReturnsEmptyString()
    {
        DateTime? date = null;
        var formattedString = date.ToFormattedString();

        Assert.Equal(string.Empty, formattedString);
    }

    [Fact]
    public void ToUtc_ReturnsUtcDateTime()
    {
        var date = new DateTime(2022, 1, 1, 12, 0, 0);
        var utcDate = date.ToUtc("Eastern Standard Time");

        Assert.Equal(new(2022, 1, 1, 17, 0, 0), utcDate);
    }

    [Fact]
    public void GetQuarter_ReturnsQuarter()
    {
        var date = new DateTime(2022, 1, 1);
        var quarter = date.GetQuarter();

        Assert.Equal(1, quarter);
    }

    [Fact]
    public void ToDateOrNull_ReturnsDateTimeOrNull()
    {
        var date = "2022-01-01";
        var dateTime = date.ToDateOrNull();

        Assert.Equal(new DateTime(2022, 1, 1), dateTime);
    }

    [Fact]
    public void ToDateOrNull_ReturnsNull()
    {
        const string date = "invalid date";
        var dateTime = date.ToDateOrNull();

        Assert.Null(dateTime);
    }

    [Fact]
    public void GetWeekOfYear_ReturnsWeekOfYear()
    {
        var date = new DateTime(2022, 1, 1);
        var weekOfYear = date.GetWeekOfYear();

        Assert.Equal(52, weekOfYear);
    }

    [Fact]
    public void FirstDateOfWeek_ReturnsFirstDateOfWeek()
    {
        var firstDateOfWeek = DateExtensions.FirstDateOfWeek(2022, 1);

        Assert.Equal(new(2022, 1, 2), firstDateOfWeek);
    }

    [Fact]
    public void GetFirstDayOfMonth_ReturnsFirstDayOfMonth()
    {
        var date = new DateTime(2022, 1, 15);
        var firstDayOfMonth = date.GetFirstDayOfMonth();

        Assert.Equal(new(2022, 1, 1), firstDayOfMonth);
    }

    [Fact]
    public void GetLastDayOfMonth_ReturnsLastDayOfMonth()
    {
        var date = new DateTime(2022, 1, 15);
        var lastDayOfMonth = date.GetLastDayOfMonth();

        Assert.Equal(new(2022, 1, 31), lastDayOfMonth);
    }

    [Fact]
    public void GetFirstBusinessDayOfMonth_ReturnsFirstBusinessDayOfMonth()
    {
        var date = new DateTime(2022, 1, 1);
        var firstBusinessDayOfMonth = date.GetFirstBusinessDayOfMonth();

        Assert.Equal(new(2022, 1, 3), firstBusinessDayOfMonth);
    }

    [Fact]
    public void GetLastBusinessDayOfMonth_ReturnsLastBusinessDayOfMonth()
    {
        var date = new DateTime(2022, 1, 31);
        var lastBusinessDayOfMonth = date.GetLastBusinessDayOfMonth();

        Assert.Equal(new(2022, 1, 31), lastBusinessDayOfMonth);
    }

    [Fact]
    public void GetBusinessDays_ReturnsBusinessDays()
    {
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 1, 31);
        var businessDays = startDate.GetBusinessDays(endDate);

        Assert.Equal(21, businessDays);
    }

    [Fact]
    public void GetMonthFromWeekYear_ReturnsMonth()
    {
        var month = 2022.GetMonthFromWeekYear(1);

        Assert.Equal(1, month);
    }

    [Fact]
    public void IsWeekend_ReturnsTrue()
    {
        var date = new DateTime(2022, 1, 1);
        var isWeekend = date.IsWeekend();

        Assert.True(isWeekend);
    }

    [Fact]
    public void IsWeekend_ReturnsFalse()
    {
        var date = new DateTime(2022, 1, 3);
        var isWeekend = date.IsWeekend();

        Assert.False(isWeekend);
    }

    [Fact]
    public void ToMidnight_ReturnsMidnight()
    {
        var date = new DateTime(2022, 1, 1, 12, 0, 0);
        var midnight = date.ToMidnight();

        Assert.Equal(new(2022, 1, 1, 23, 59, 59), midnight);
    }

    [Fact]
    public void GetDaysInMonth_ReturnsDaysInMonth()
    {
        var date = new DateTime(2022, 1, 1);
        var daysInMonth = date.GetDaysInMonth();

        Assert.Equal(31, daysInMonth);
    }

    [Fact]
    public void DiffMonths_ReturnsDiffMonths()
    {
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 3, 1);
        var diffMonths = endDate.DiffMonths(startDate);

        Assert.Equal(2, diffMonths);
    }

    [Fact]
    public void OrUtcNow_ReturnsUtcNow()
    {
        var date = new DateTime(2022, 1, 1, 12, 0, 0);
        var utcNow = DateTime.UtcNow;
        var orUtcNow = date.OrUtcNow();

        Assert.True(orUtcNow <= utcNow);
    }

    [Fact]
    public void OrNow_ReturnsNow()
    {
        var date = new DateTime(2022, 1, 1, 12, 0, 0);
        var now = DateTime.Now;
        var orNow = date.OrNow();

        Assert.True(orNow <= now);
    }

    [Fact]
    public void OrToday_ReturnsToday()
    {
        var date = new DateTime(2022, 1, 1, 12, 0, 0);
        var today = DateTime.Today;
        var orToday = date.OrToday();

        Assert.True(orToday <= today);
    }

    [Fact]
    public void GetNextBusinessDay_ReturnsNextBusinessDay()
    {
        var date = new DateTime(2022, 1, 1);
        var nextBusinessDay = date.GetNextBusinessDay();

        Assert.Equal(new(2022, 1, 3), nextBusinessDay);
    }

    [Fact]
    public void GetDateFromWeekDay_ReturnsDateFromWeekDay()
    {
        var date = new DateTime(2022, 1, 1);
        var dateFromWeekDay = date.GetDateFromWeekDay(1);

        Assert.Equal(new(2022, 1, 3), dateFromWeekDay);
    }
}