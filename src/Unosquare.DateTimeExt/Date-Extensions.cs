namespace Unosquare.DateTimeExt;

public static class DateExtensions
{
    private const string DateFormatNotifications = "MMMM d, yyyy";

    public static string ToFormattedString(this DateTime @this) =>
        @this.ToString(DateFormatNotifications, CultureInfo.InvariantCulture);

    public static string ToFormattedString(this DateTime? @this) => @this == null
        ? string.Empty
        : @this.Value.ToFormattedString();

    public static DateTime ToUtc(
        this DateTime @this,
        string timezone)
    {
        try
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return TimeZoneInfo.ConvertTimeToUtc(@this, timezoneInfo);
        }
        catch
        {
            return TimeZoneInfo.ConvertTimeToUtc(@this, TimeZoneInfo.Local);
        }
    }

    public static int GetQuarter(this DateTime @this) => (@this.Month - 1) / 3 + 1;

    public static (DateTime CurrentQuarter, DateTime LastQuarter) GetCurrentAndLastQuarterStartDates(
        this DateTime @this)
    {
        var quarterNumber = @this.GetQuarter();
        return GetQuarterDateRange(quarterNumber);
    }

    public static (DateTime CurrentQuarter, DateTime LastQuarter) GetQuarterDateRange(this int @this,
        int? year = null)
    {
        var targetYear = year ?? DateTime.UtcNow.Year;
        var currentQuarter = new DateTime(targetYear, (@this - 1) * 3 + 1, 1);
        return (currentQuarter, currentQuarter.AddMonths(-3));
    }

    public static DateTime? ToDateOrNull(this string @this) =>
        !DateTime.TryParse(@this, out var value) ? null : value;

    public static int GetWeekOfYear(this DateTime time)
    {
        var dfi = DateTimeFormatInfo.InvariantInfo;

        return dfi.Calendar.GetWeekOfYear(
            time,
            CalendarWeekRule.FirstFullWeek,
            dfi.FirstDayOfWeek);
    }

    public static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        var jan1 = new DateTime(year, 1, 1);
        var firstDayOfFirstWeek = jan1;

        if (DateTimeFormatInfo.InvariantInfo.FirstDayOfWeek != jan1.DayOfWeek)
        {
            var daysOffset = (int)DateTimeFormatInfo.InvariantInfo.FirstDayOfWeek - (int)jan1.DayOfWeek;
            firstDayOfFirstWeek = jan1.AddDays(daysOffset + 7);
        }

        return firstDayOfFirstWeek.AddDays((weekOfYear - 1) * 7);
    }

    public static DateRange GetMonthRange(this int year, int month) =>
        new DateTime(year, month, 1).GetMonthRange();

    public static DateRange GetMonthRange(this DateTime date)
    {
        var startDate = date.GetFirstDayOfMonth();
        var endDate = startDate.GetLastDayOfMonth();

        return new(startDate, endDate);
    }

    public static DateTime GetFirstDayOfMonth(this DateTime date) => new(date.Year, date.Month, 1);

    public static DateTime GetLastDayOfMonth(this DateTime date) =>
        new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

    public static DateTime GetFirstBusinessDayOfMonth(this DateTime @this)
    {
        var firstDayOfCurrentMonth = @this.GetFirstDayOfMonth();

        return firstDayOfCurrentMonth.DayOfWeek switch
        {
            DayOfWeek.Sunday => firstDayOfCurrentMonth.AddDays(1),
            DayOfWeek.Saturday => firstDayOfCurrentMonth.AddDays(2),
            _ => firstDayOfCurrentMonth
        };
    }

    public static DateTime GetLastBusinessDayOfMonth(this DateTime @this)
    {
        var lastDayOfCurrentMonth = @this.GetLastDayOfMonth();

        return lastDayOfCurrentMonth.DayOfWeek switch
        {
            DayOfWeek.Sunday => lastDayOfCurrentMonth.AddDays(-2),
            DayOfWeek.Saturday => lastDayOfCurrentMonth.AddDays(-1),
            _ => lastDayOfCurrentMonth
        };
    }

    public static int GetBusinessDays(this DateTime startDate, DateTime endDate)
    {
        var calcBusinessDays =
            1 + ((endDate - startDate).TotalDays * 5 -
                 (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

        if (endDate.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
        if (startDate.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

        return (int)calcBusinessDays;
    }

    public static int GetMonthFromWeekYear(this int year, int week)
    {
        var firstSaturday = new DateTime(year, 1, 1);

        while (firstSaturday.DayOfWeek != DayOfWeek.Saturday)
            firstSaturday = firstSaturday.AddDays(1);

        var weekOfFirstSaturday = firstSaturday.GetWeekOfYear();

        var date = weekOfFirstSaturday == 1 ? firstSaturday.AddDays((week - 1) * 7) : firstSaturday.AddDays(week * 7);

        return date.Month;
    }

    public static bool IsWeekend(this DateTime date) =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    public static int GetDaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);

    public static int DiffMonths(this DateTime endDate, DateTime startDate)
    {
        var diffYears = Math.Abs(endDate.Year - startDate.Year);
        var diffMonths = endDate.Month - startDate.Month;
        return (diffYears * 12) + diffMonths;
    }

    public static YearQuarter ToYearQuarter(this DateTime date) => new(date);
    public static YearMonth ToYearMonth(this DateTime date) => new(date);
    public static YearWeek ToYearWeek(this DateTime date) => new(date);
    public static MonthToDate ToMonthToDate(this DateTime date) => new(date);
    public static YearToDate ToYearToDate(this DateTime date) => new(date.Year);
    public static TrailingTwelveMonths ToTrailingTwelveMonths(this DateTime date) => new(date);
}