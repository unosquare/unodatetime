namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasQuarters
{
    IReadOnlyCollection<int> Quarters { get; }
}