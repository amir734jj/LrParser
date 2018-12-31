using System.Collections.Immutable;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Grammar;

namespace Core.Models.Interfaces.Parser
{
    public interface IState
    {
        int Id { get; set; }
        
        ImmutableDictionary<IProduction, int> Rules { get; set; }
        
        ImmutableList<IAction> Actions { get; set; }
    }
}