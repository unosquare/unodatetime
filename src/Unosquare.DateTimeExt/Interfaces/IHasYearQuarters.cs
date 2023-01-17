namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasYearQuarters
{
    IReadOnlyCollection<YearQuarter> YearQuarters { get; }
}