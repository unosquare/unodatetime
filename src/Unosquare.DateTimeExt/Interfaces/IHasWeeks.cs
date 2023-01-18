namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasWeeks
{
    IReadOnlyCollection<int> Weeks { get; }
}