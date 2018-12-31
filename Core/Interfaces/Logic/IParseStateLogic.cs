using System.Collections.Immutable;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Interfaces.Logic
{
    public interface IParseStateLogic
    {
        ImmutableDictionary<IProduction, int> HandleClosure(INonTerminal nonTerminal);
        
        ImmutableDictionary<IProduction, int> HandleClosure(IProduction currentProduction, int marker);
        
        ImmutableHashSet<IState> ResolveStates();
    }
}