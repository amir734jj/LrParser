using System.Collections.Generic;
using System.Collections.Immutable;

namespace Core.Models.Interfaces
{
    public interface IProduction
    {
        int Order { get; set; }
        
        INonTerminal NonTerminal { get; set; }
        
        ImmutableArray<ITerminalOrNonTerminal> Items { get; set; }
    }
}