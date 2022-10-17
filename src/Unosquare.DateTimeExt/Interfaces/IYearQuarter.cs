namespace Unosquare.DateTimeExt.Interfaces;

public interface IYearQuarter : IHasReadOnlyYear
{
    int Quarter { get; }
}