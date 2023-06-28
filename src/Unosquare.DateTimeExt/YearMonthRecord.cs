using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record YearMonthRecord : IYearMonth
{
    public int Year { get; set; }
    public int Month { get; set; }
}