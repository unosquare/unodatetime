using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

/// <summary>
/// Represents a range of dates with a start and end date.
/// </summary>
public class DateRange : RangeBase<DateTime>, IReadOnlyDateRange, IHasReadOnlyMidnightEndDate, IComparable<DateRange>, IEnumerable<DateTime>, IHasBusinessDays
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRange"/> class with the current UTC date and time.
    /// </summary>
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

    public DateRangeRecord ToRecord() => new(StartDate, EndDate);

    public override string ToString() => $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";

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

    public static bool operator <(DateRange left, DateRange right) => left.StartDate < right.StartDate;

    public static bool operator >=(DateRange left, DateRange right) => left.StartDate >= right.StartDate;

    public static bool operator <=(DateRange left, DateRange right) => left.StartDate <= right.StartDate;

    public static bool operator !=(DateRange left, DateRange right) => !(left == right);

    public static bool TryParse(string? value, out DateRange result)
    {
        result = default!;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = value.Split(" - ");

        if (parts.Length != 2)
            return false;

        if (!DateTime.TryParse(parts[0], out var startDate))
            return false;

        if (!DateTime.TryParse(parts[1], out var endDate))
            return false;

        result = new(startDate, endDate);

        return true;
    }
}