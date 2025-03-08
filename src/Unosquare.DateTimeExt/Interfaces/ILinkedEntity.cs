namespace Unosquare.DateTimeExt.Interfaces;

public interface ILinkedEntity<out T>
{
    T Previous(int offset = 1);
    T Next(int offset = 1);

    bool IsCurrent { get; }
}
