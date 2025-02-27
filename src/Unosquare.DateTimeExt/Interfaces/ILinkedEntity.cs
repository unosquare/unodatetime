namespace Unosquare.DateTimeExt.Interfaces;

public interface ILinkedEntity<out T>
{
    T Previous { get; }
    T Next { get; }

    bool IsCurrent { get; }
}