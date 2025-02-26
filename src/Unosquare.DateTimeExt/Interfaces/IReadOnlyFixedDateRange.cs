namespace Unosquare.DateTimeExt.Interfaces;

public interface IReadOnlyFixedDateRange<out T> : IReadOnlyDateRange
{
    T Previous { get; }
    T Next { get; }

    bool IsCurrent { get; }
}