using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class OpenDateRange(DateTime startDate, DateTime? endDate = null) : RangeBase<DateTime, DateTime?>(startDate, endDate), IReadOnlyOpenDateRange, IComparable<OpenDateRange>, IEnumerable<DateTime>, IHasBusinessDays
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override IEnumerator<DateTime> GetEnumerator()
    {
        if (EndDate is not null)
        {
            var daysInBetween = (EndDate.Value - StartDate).Days;
            for (var i = 0; i <= daysInBetween; i++)
                yield return StartDate.AddDays(i);
        }
        else
        {
            var i = 0;

            while (DateTime.MaxValue != StartDate.AddDays(i))
                yield return StartDate.AddDays(++i);
        }
    }

    public int CompareTo(OpenDateRange? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        if (other is null)
            return 1;

        var startDateComparison = StartDate.CompareTo(other.StartDate);

        return startDateComparison != 0 ? startDateComparison : (EndDate?.CompareTo(other.EndDate) ?? 0);
    }

    public override string ToString() => $"{StartDate.ToShortDateString()} - {EndDate?.ToShortDateString()}";

    public override int GetHashCode() => StartDate.GetHashCode() + EndDate.GetHashCode();

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();
    public DateTime LastBusinessDay => (EndDate ?? DateTime.Now).GetLastBusinessDayOfMonth();
}