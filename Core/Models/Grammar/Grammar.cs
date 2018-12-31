using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Grammar
{
    public class Grammar : IGrammar
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ImmutableList<IProduction> Productions { get; set; }
        
        public ImmutableArray<ITerminal> Terminals { get; set; }
        
        public ImmutableArray<INonTerminal> NonTerminals { get; set; }
        
        public ImmutableArray<ITerminalOrNonTerminal> TerminalsAndNonTerminals { get; set; }
        
        public INonTerminal Start { get; set; }
        public ITerminal Epsilon { get; set; }
        public KeyValuePair<INonTerminal, ITerminal> Eof { get; set; }

        public override string ToString()
        {
            var str = string.Empty;

            str += $"Start: {Start}" + Environment.NewLine;
            str += $"Epsilon: {Epsilon}" + Environment.NewLine;
            
            str += string.Join(Environment.NewLine, Productions.OrderByDescending(x => x.Order));

            return str;
        }
    }
}