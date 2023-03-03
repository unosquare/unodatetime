namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasBusinessDaysDateOnly
{
    DateOnly FirstBusinessDay { get; }
    DateOnly LastBusinessDay { get; }
}