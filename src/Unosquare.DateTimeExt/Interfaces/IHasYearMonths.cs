namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasYearMonths
{
    IReadOnlyCollection<YearMonth> YearMonths { get; }
}