using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

/// <summary>
/// Represents a range of dates with only the date component (no time component).
/// </summary>
public sealed class DateOnlyRange : RangeBase<DateOnly>, IReadOnlyDateOnlyRange, IComparable<DateOnlyRange>,
    IEnumerable<DateOnly>, IHasBusinessDaysDateOnly
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
    /// The range is initialized to the current UTC date.
    /// </summary>
    public DateOnlyRange()
        : this(DateOnly.FromDateTime(DateTime.UtcNow))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date (optional).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the end date is before the start date.</exception>
    public DateOnlyRange(DateOnly startDate, DateOnly? endDate = null)
        : base(startDate, endDate)
    {
        if (EndDate < StartDate)
            throw new ArgumentOutOfRangeException(nameof(endDate), "End Date should be after Start Date");
    }

    public DateOnly FirstBusinessDay =>
        DateOnly.FromDateTime(StartDate.ToDateTime(TimeOnly.MinValue).GetFirstBusinessDayOfMonth());

    public DateOnly LastBusinessDay =>
        DateOnly.FromDateTime(EndDate.ToDateTime(TimeOnly.MinValue).GetLastBusinessDayOfMonth());

    public int DaysInBetween => (EndDate.DayNumber - StartDate.DayNumber);

    public override string ToString() => $"{StartDate.ToShortDateString()}-{EndDate.ToShortDateString()}";

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode() => StartDate.GetHashCode() + EndDate.GetHashCode();

    public override IEnumerator<DateOnly> GetEnumerator()
    {
        for (var i = 0; i <= DaysInBetween; i++)
            yield return StartDate.AddDays(i);
    }

    public override bool Equals(object? obj) =>
        (obj is DateOnlyRange other) && other.StartDate == StartDate && other.EndDate == EndDate;

    public int CompareTo(DateOnlyRange? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        if (other is null)
            return 1;

        var startDateComparison = StartDate.CompareTo(other.StartDate);

        return startDateComparison != 0 ? startDateComparison : EndDate.CompareTo(other.EndDate);
    }

    public static bool operator ==(DateOnlyRange? left, DateOnlyRange? right) => left?.Equals(right) ?? right is null;

    public static bool operator >(DateOnlyRange left, DateOnlyRange right) => left.StartDate > right.StartDate;

    public static bool operator <(DateOnlyRange left, DateOnlyRange right) => left.EndDate < right.EndDate;

    public static bool operator !=(DateOnlyRange left, DateOnlyRange right) => !(left == right);

    public static bool operator <=(DateOnlyRange left, DateOnlyRange right) => left.CompareTo(right) <= 0;

    public static bool operator >=(DateOnlyRange left, DateOnlyRange right) => left.CompareTo(right) >= 0;
}