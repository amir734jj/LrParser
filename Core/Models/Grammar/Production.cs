using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models.Interfaces;

namespace Core.Models
{
    public class Production : IProduction
    {
        public INonTerminal NonTerminal { get; set; }
        
        public ImmutableList<ITerminalOrNonTerminal> Items { get; set; }
    }
}