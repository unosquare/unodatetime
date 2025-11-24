namespace Unosquare.DateTimeExt;

public static class DateExtensions
{
    private const string DateFormatNotifications = "MMMM d, yyyy";

    public static string ToFormattedString(this DateTime @this) =>
        @this.ToString(DateFormatNotifications, CultureInfo.InvariantCulture);

    public static string ToFormattedString(this DateTime? @this) => @this == null
        ? string.Empty
        : @this.Value.ToFormattedString();

    extension(DateTime @this)
    {
        public DateTime ToUtc(string timezone)
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

        public int GetQuarter() => (@this.Month - 1) / 3 + 1;
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

    extension(DateTime date)
    {
        public DateTime GetFirstDayOfMonth() => new(date.Year, date.Month, 1);

        public DateTime GetLastDayOfMonth() =>
            new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        public DateTime GetFirstBusinessDayOfMonth()
        {
            var firstDayOfCurrentMonth = date.GetFirstDayOfMonth();

            return firstDayOfCurrentMonth.DayOfWeek switch
            {
                DayOfWeek.Sunday => firstDayOfCurrentMonth.AddDays(1),
                DayOfWeek.Saturday => firstDayOfCurrentMonth.AddDays(2),
                _ => firstDayOfCurrentMonth,
            };
        }

        public DateTime GetLastBusinessDayOfMonth()
        {
            var lastDayOfCurrentMonth = date.GetLastDayOfMonth();

            return lastDayOfCurrentMonth.DayOfWeek switch
            {
                DayOfWeek.Sunday => lastDayOfCurrentMonth.AddDays(-2),
                DayOfWeek.Saturday => lastDayOfCurrentMonth.AddDays(-1),
                _ => lastDayOfCurrentMonth,
            };
        }

        public int GetBusinessDays(DateTime endDate)
        {
            var calcBusinessDays =
                1 + ((endDate - date).TotalDays * 5 -
                     (date.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if (endDate.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (date.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return (int)calcBusinessDays;
        }
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

    extension(DateTime date)
    {
        public bool IsWeekend() =>
            date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        public DateTime ToMidnight() =>
            date.Date.AddDays(1).AddSeconds(-1);

        public int GetDaysInMonth() => DateTime.DaysInMonth(date.Year, date.Month);

        public int DiffMonths(DateTime startDate)
        {
            var diffYears = Math.Abs(date.Year - startDate.Year);
            var diffMonths = date.Month - startDate.Month;

            return (diffYears * 12) + diffMonths;
        }

        public DateTime OrUtcNow() =>
            date > DateTime.UtcNow ? DateTime.UtcNow : date;

        public DateTime OrNow() =>
            date > DateTime.Now ? DateTime.Now : date;

        public DateTime OrToday() =>
            date > DateTime.Today ? DateTime.Today : date;

        public DateTime GetNextBusinessDay()
        {
            do
            {
                date = date.AddDays(1);
            } while (date.IsWeekend());

            return date;
        }

        public DateTime GetDateFromWeekDay(int weekday)
        {
            var daysAdded = 0;

            while (daysAdded < weekday)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    daysAdded++;
            }

            return date;
        }
    }
}