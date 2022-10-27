using System.Collections.ObjectModel;

namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasMonths
{
    ReadOnlyCollection<int> Months { get; }
}