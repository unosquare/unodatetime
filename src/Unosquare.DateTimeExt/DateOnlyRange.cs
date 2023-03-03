using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public sealed class DateOnlyRange : RangeBase<DateOnly>, IReadOnlyDateOnlyRange, IComparable<DateOnlyRange>,
    IEnumerable<DateOnly>, IHasBusinessDaysDateOnly
{
    public DateOnlyRange()
        : this(DateOnly.FromDateTime(DateTime.UtcNow))
    {
    }

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
}