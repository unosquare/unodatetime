using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record DateRangeRecord(DateTime StartDate, DateTime EndDate) : IReadOnlyDateRange;

public record YearMonthRecord(int Year, int Month) : IYearMonth;

public record YearQuarterRecord(int Year, int Quarter) : IYearQuarter;

public record YearWeekRecord(int Year, int Week) : IYearWeek;