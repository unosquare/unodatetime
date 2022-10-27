using System.Collections.ObjectModel;

namespace Unosquare.DateTimeExt.Interfaces;

public interface IHasWeeks
{
    ReadOnlyCollection<int> Weeks { get; }
}