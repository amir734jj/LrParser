using System.Collections.Generic;
using System.Collections.Immutable;

namespace Core.Models.Interfaces
{
    public interface IGrammar
    {
        ImmutableList<IProduction> Productions { get; set; }

        ImmutableArray<ITerminal> Terminals { get; set; }

        ImmutableArray<INonTerminal> NonTerminals { get; set; }

        ImmutableArray<ITerminalOrNonTerminal> TerminalsAndNonTerminals { get; set; }
        
        INonTerminal Start { get; set; }
        ITerminal Epsilon { get; set; }
        
        KeyValuePair<INonTerminal, ITerminal> Eof { get; set; }
    }
}