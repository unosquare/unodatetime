using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record YearQuarterRecord : IYearQuarter
{
    public int Year { get; set; }
    public int Quarter { get; set; }
}