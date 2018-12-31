using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Interfaces.Builders;
using Core.Models.Actions;
using Core.Models.Grammar;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Nodes;

namespace Core.Builders
{
    public static class GrammarBuilder
    {
        public static GrammarBuilderInit<TNonTerminal, TTerminal> New<TNonTerminal, TTerminal>()
            where TNonTerminal : Enum
            where TTerminal : Enum
        {
            return new GrammarBuilderInit<TNonTerminal, TTerminal>();
        }
    }

    public class GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        protected IGrammar Grammar;

        /// <summary>
        /// Given terminal name enum, returns the instance
        /// </summary>
        /// <param name="terminalName"></param>
        /// <returns></returns>
        protected ITerminal FindTerminal(TTerminal terminalName)
        {
            return Grammar.Terminals.FirstOrDefault(x => Equals(x.Name, terminalName)) ?? throw new Exception();
        }

        /// <summary>
        /// Given non-terminal name enum, returns the instance
        /// </summary>
        /// <param name="nonTerminalName"></param>
        /// <returns></returns>
        protected INonTerminal FindNonTerminal(TNonTerminal nonTerminalName)
        {
            return Grammar.NonTerminals.FirstOrDefault(x => Equals(x.Name, nonTerminalName)) ?? throw new Exception();
        }
    }

    public class GrammarBuilderInit<TNonTerminal, TTerminal> : GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {

        public GrammarBuilderInit()
        {
            var nonTerminalEnums = (TNonTerminal[]) Enum.GetValues(typeof(TNonTerminal));
            var terminalEnums = (TTerminal[]) Enum.GetValues(typeof(TTerminal));

            var productions = ImmutableList<IProduction>.Empty;
            var nonTerminals = nonTerminalEnums.Select(x => (INonTerminal) new NonTerminal {Name = x}).ToList();
            var terminals = terminalEnums.Select(x => (ITerminal) new Terminal {Name = x}).ToList();
            var terminalsAndNonTerminals = terminals.Concat(nonTerminals.Cast<ITerminalOrNonTerminal>()).ToList();

            Grammar = new Grammar
            {
                Productions = productions,
                NonTerminals = nonTerminals.ToImmutableArray(),
                Terminals = terminals.ToImmutableArray(),
                TerminalsAndNonTerminals = terminalsAndNonTerminals.ToImmutableArray()
            };
        }

        public GrammarBuilderStart<TNonTerminal, TTerminal> Init()
        {
            return new GrammarBuilderStart<TNonTerminal, TTerminal>(Grammar);
        }
    }
    
    public class ProductionBuilderStruct<TNonTerminal, TTerminal> : IProductionBuilderStruct<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        public Action<TNonTerminal> NonTerminal { get; }
        public Action<TTerminal> Terminal { get; }
        
        public Action<int> Order { get; }

        public ProductionBuilderStruct(Action<TNonTerminal> nonTerminal, Action<TTerminal> terminal, Action<int> order)
        {
            NonTerminal = nonTerminal;
            Terminal = terminal;
            Order = order;
        }
    }

    public class GrammarBuilderStart<TNonTerminal, TTerminal> : GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        public GrammarBuilderStart(IGrammar grammar)
        {
            Grammar = grammar;
        }

        public GrammarBuilderEpsilon<TNonTerminal, TTerminal> WithStartSymbol(TNonTerminal nonTerminal)
        {
            Grammar.Start = FindNonTerminal(nonTerminal);
            
            return new GrammarBuilderEpsilon<TNonTerminal, TTerminal>(Grammar);
        }
    }
    
    public class GrammarBuilderEpsilon<TNonTerminal, TTerminal> : GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        public GrammarBuilderEpsilon(IGrammar grammar)
        {
            Grammar = grammar;
        }

        public GrammarBuilderEof<TNonTerminal, TTerminal> WithEpsilon(TTerminal terminal)
        {
            Grammar.Epsilon = FindTerminal(terminal);
            
            return new GrammarBuilderEof<TNonTerminal, TTerminal>(Grammar);
        }
    }
    
    public class GrammarBuilderEof<TNonTerminal, TTerminal> : GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        public GrammarBuilderEof(IGrammar grammar)
        {
            Grammar = grammar;
        }

        public GrammarBuilderProduction<TNonTerminal, TTerminal> WithEof(TNonTerminal root, TTerminal terminal)
        {
            Grammar.Eof = new KeyValuePair<INonTerminal, ITerminal>(FindNonTerminal(root), FindTerminal(terminal));
            
            return new GrammarBuilderProduction<TNonTerminal, TTerminal>(Grammar);
        }
    }

    public class GrammarBuilderProduction<TNonTerminal, TTerminal> : GrammarBuilderBase<TNonTerminal, TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        public GrammarBuilderProduction(IGrammar grammar)
        {
            Grammar = grammar;
        }

        /// <summary>
        /// Adds production to grammar
        /// </summary>
        /// <param name="nonTerminal"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public GrammarBuilderProduction<TNonTerminal, TTerminal> WithProduction(TNonTerminal nonTerminal,
            Action<ProductionBuilderStruct<TNonTerminal, TTerminal>> handler)
        {
            var production = new Production
            {
                NonTerminal = Grammar.NonTerminals.First(x => Equals(x.Name, nonTerminal)),
                Items = ImmutableArray<ITerminalOrNonTerminal>.Empty,
                Order = 0
            };

            void TerminalHandler(TTerminal terminalName)
            {
                production.Items = production.Items.Add(FindTerminal(terminalName));
            }

            void NonTerminalHandler(TNonTerminal nonTerminalName)
            {
                production.Items = production.Items.Add(FindNonTerminal(nonTerminalName));
            }

            // ReSharper disable once ImplicitlyCapturedClosure
            void OrderHandler(int order)
            {
                production.Order = order;
            }

            handler(new ProductionBuilderStruct<TNonTerminal, TTerminal>(NonTerminalHandler, TerminalHandler, OrderHandler));

            Grammar.Productions = Grammar.Productions.Add(production);

            return this;
        }

        /// <summary>
        /// Returns the built grammar
        /// </summary>
        /// <returns></returns>
        public IGrammar Build()
        {
            return Grammar;
        }
    }
}