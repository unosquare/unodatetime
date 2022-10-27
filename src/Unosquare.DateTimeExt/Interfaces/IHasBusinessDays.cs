namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasBusinessDays
{
    DateTime FirstBusinessDay { get; }
    DateTime LastBusinessDay { get; }
}