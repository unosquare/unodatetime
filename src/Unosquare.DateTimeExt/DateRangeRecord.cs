using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public record DateRangeRecord : IReadOnlyDateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}