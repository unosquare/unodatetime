using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record DateRangeRecord : IReadOnlyDateRange
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public record YearMonthRecord : IYearMonth
{
    public int Year { get; init; }
    public int Month { get; init; }
}

public record YearQuarterRecord : IYearQuarter
{
    public int Year { get; init; }
    public int Quarter { get; init; }
}

public record YearWeekRecord : IYearWeek
{
    public int Year { get; init; }
    public int Week { get; init; }
}