using System.Collections.Immutable;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Interfaces.Grammar
{
    public interface IProduction
    {
        int Order { get; set; }
        
        INonTerminal NonTerminal { get; set; }
        
        ImmutableArray<ITerminalOrNonTerminal> Items { get; set; }
    }
}