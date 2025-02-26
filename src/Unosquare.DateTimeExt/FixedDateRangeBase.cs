using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class FixedDateRangeBase<T>(DateTime startDate, DateTime endDate)
    : DateRange(startDate, endDate), IReadOnlyFixedDateRange<T>
{
    public abstract T Previous { get; }
    public abstract T Next { get; }
    public abstract bool IsCurrent { get; }
}