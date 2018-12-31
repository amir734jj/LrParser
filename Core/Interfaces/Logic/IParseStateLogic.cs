using System.Collections.Immutable;
using Core.Models.Interfaces;

namespace Core.Interfaces.Logic
{
    public interface IParseStateLogic
    {
        ImmutableDictionary<IProduction, int> HandleClosure(INonTerminal nonTerminal);
        
        ImmutableDictionary<IProduction, int> HandleClosure(IProduction currentProduction, int marker);
        
        ImmutableHashSet<IState> ResolveStates();
    }
}