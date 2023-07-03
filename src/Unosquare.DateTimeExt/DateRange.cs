using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class DateRange : RangeBase<DateTime>, IReadOnlyDateRange, IHasReadOnlyMidnightEndDate, IComparable<DateRange>, IEnumerable<DateTime>, IHasBusinessDays
{
    public DateRange()
        : this(DateTime.UtcNow)
    {
    }

    public DateRange(IReadOnlyDateRange range)
        : this(range.StartDate, range.EndDate)
    {
    }

    public DateRange(DateTime startDate, DateTime? endDate = null)
    : base(startDate, endDate)
    {
        if (EndDate < StartDate)
            throw new ArgumentOutOfRangeException(nameof(endDate), "End Date should be after Start Date");
    }

    public DateTime MidnightEndDate => EndDate.Date.AddDays(1).AddSeconds(-1);

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();

    public DateTime LastBusinessDay => EndDate.GetLastBusinessDayOfMonth();

    public int DaysInBetween => (EndDate - StartDate).Days;

    public bool Contains(DateTime date) => StartDate <= date && EndDate >= date;

    public DateRangeRecord ToRecord() => new() { StartDate = StartDate, EndDate = EndDate };

    public override string ToString() => $"{StartDate.ToShortDateString()}-{EndDate.ToShortDateString()}";

    public override int GetHashCode() => StartDate.GetHashCode() + EndDate.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override IEnumerator<DateTime> GetEnumerator()
    {
        for (var i = 0; i <= DaysInBetween; i++)
            yield return StartDate.AddDays(i);
    }

    public override bool Equals(object? obj) =>
        (obj is DateRange other) && other.StartDate == StartDate && other.EndDate == EndDate;

    public int CompareTo(DateRange? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        if (other is null)
            return 1;

        var startDateComparison = StartDate.CompareTo(other.StartDate);

        return startDateComparison != 0 ? startDateComparison : EndDate.CompareTo(other.EndDate);
    }

    public static bool operator ==(DateRange? left, DateRange? right) => left?.Equals(right) ?? right is null;

    public static bool operator >(DateRange left, DateRange right) => left.StartDate > right.StartDate;

    public static bool operator <(DateRange left, DateRange right) => left.EndDate < right.EndDate;

    public static bool operator !=(DateRange left, DateRange right) => !(left == right);
}