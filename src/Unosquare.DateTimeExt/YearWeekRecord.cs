using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record YearWeekRecord : IYearWeek
{
    public int Year { get; set; }
    public int Week { get; set; }
}