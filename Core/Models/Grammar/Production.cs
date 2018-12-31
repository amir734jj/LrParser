using System.Collections.Immutable;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Grammar
{
    public class Production : IProduction
    {
        public int Order { get; set; }
        
        public INonTerminal NonTerminal { get; set; }
        
        public ImmutableArray<ITerminalOrNonTerminal> Items { get; set; }

        public override string ToString()
        {
            return $"{NonTerminal.Name,8}{"->", 4}{string.Join(' ', Items),20}{$"({Order})",8}";
        }
    }
}