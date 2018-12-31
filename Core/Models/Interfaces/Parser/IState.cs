using System.Collections.Generic;
using System.Collections.Immutable;

namespace Core.Models.Interfaces
{
    public interface IState
    {
        int Id { get; set; }
        
        ImmutableDictionary<IProduction, int> Rules { get; set; }
        
        ImmutableList<IAction> Actions { get; set; }
    }
}