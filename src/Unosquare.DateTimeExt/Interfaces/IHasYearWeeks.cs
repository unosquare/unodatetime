namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasYearWeeks
{
    IReadOnlyCollection<YearWeek> YearWeeks { get; }
}