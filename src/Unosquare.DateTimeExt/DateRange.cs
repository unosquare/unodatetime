using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class DateRange : IReadOnlyDateRange, IComparable<DateRange>, IEnumerable<DateTime>
{
    public DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public override string ToString() => $"{StartDate}-{EndDate}";

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode() => StartDate.GetHashCode() + EndDate.GetHashCode();

    public IEnumerator<DateTime> GetEnumerator()
    {
        var daysDifference = (EndDate - StartDate).Days;

        for (var i = 0; i <= daysDifference; i++)
            yield return StartDate.AddDays(i);
    }

    public override bool Equals(object? obj) =>
        (obj is DateRange other) && other.StartDate == StartDate && other.EndDate == EndDate;

    public int CompareTo(DateRange? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        if (ReferenceEquals(null, other))
            return 1;

        var startDateComparison = StartDate.CompareTo(other.StartDate);

        return startDateComparison != 0 ? startDateComparison : EndDate.CompareTo(other.EndDate);
    }
}