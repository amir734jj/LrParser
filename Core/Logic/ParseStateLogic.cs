using System;
using System.Collections.Immutable;
using System.Linq;
using Core.Interfaces.Logic;
using Core.Models.Actions;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;
using Core.Models.Parser;
using Core.Models.Pipeline;

namespace Core.Logic
{
    public class ParseStateLogic : IParseStateLogic
    {
        private readonly PipelinePayload _pipelinePayload;

        public ParseStateLogic(PipelinePayload pipelinePayload)
        {
            _pipelinePayload = pipelinePayload;
        }

        public ImmutableDictionary<IProduction, int> HandleClosure(INonTerminal nonTerminal)
        {
            var result = ImmutableDictionary<IProduction, int>.Empty;

            foreach (var production in _pipelinePayload.AugmentedGrammar.Productions)
            {
                if (production.NonTerminal == nonTerminal && !result.ContainsKey(production))
                {
                    result = result.Add(production, 0);
                }
            }

            return result;
        }

        public ImmutableDictionary<IProduction, int> HandleClosure(IProduction currentProduction, int marker)
        {
            var result = ImmutableDictionary<IProduction, int>.Empty;

            result = result.Add(currentProduction, marker);

            // ReSharper disable once InvertIf
            if (marker < currentProduction.Items.Length && currentProduction.Items[marker] is INonTerminal nonTerminal)
            {
                foreach (var production in _pipelinePayload.AugmentedGrammar.Productions)
                {
                    if (production.NonTerminal == nonTerminal && !result.ContainsKey(production))
                    {
                        result = result.Add(production, 0);
                    }
                }
            }

            return result;
        }

        public ImmutableHashSet<IState> ResolveStates()
        {
            var visitedStates = ImmutableList<IState>.Empty;
            var states = ImmutableHashSet<IState>.Empty;
            var rootProduction = _pipelinePayload.AugmentedGrammar.Productions.Find(x =>
                x.Items.Contains(_pipelinePayload.AugmentedGrammar.Eof.Value));

            // Initial state with marker set to -1 index
            var initialState = new State
            {
                Id = states.Count,
                Actions = ImmutableList<IAction>.Empty,
                Rules = HandleClosure(_pipelinePayload.AugmentedGrammar.Start)
                    .Add(rootProduction, 0)
            };

            states = states.Add(initialState);

            IState currentState = initialState;

            do
            {
                foreach (var (key, value) in currentState.Rules)
                {
                    if (key == rootProduction) continue;

                    // Add Reduce rule if production reached the end
                    if (key.Items.Length == value)
                    {
                        var action = new Reduce
                        {
                            Production = key,
                            NonTerminal = key.NonTerminal
                        };

                        currentState.Actions = currentState.Actions.Add(action);
                    }

                    foreach (var terminalOrNonTerminal in _pipelinePayload.AugmentedGrammar.TerminalsAndNonTerminals)
                    {
                        // ReSharper disable once InvertIf
                        if (value < key.Items.Length && key.Items[value] == terminalOrNonTerminal)
                        {
                            var nextState =
                                states.FirstOrDefault(x => x != currentState && x.Rules.Any(y =>
                                {
                                    var (production, index) = y;

                                    var condition1 = production == key;
                                    var condition2 = value < production.Items.Length &&
                                                     production.Items[value] == terminalOrNonTerminal;

                                    return (condition1 || condition2) && index == value + 1;
                                })) ??
                                new State
                                {
                                    Id = states.Count,
                                    Rules = HandleClosure(key, value + 1),
                                    Actions = ImmutableList<IAction>.Empty
                                };

                            if (!nextState.Rules.ContainsKey(key))
                            {
                                nextState.Rules = nextState.Rules.Add(key, value + 1);
                            }

                            states = states.Add(nextState);

                            IAction action;

                            switch (terminalOrNonTerminal)
                            {
                                case INonTerminal nonTerminal:
                                    // Add goto the next state
                                    action = new Goto
                                    {
                                        Destination = nextState,
                                        NonTerminal = nonTerminal
                                    };
                                    break;
                                case ITerminal terminal:
                                    action = new Shift
                                    {
                                        Terminal = terminal,
                                        Destination = nextState
                                    };
                                    break;
                                default:
                                    throw new Exception();
                            }

                            currentState.Actions = currentState.Actions.Add(action);
                        }
                    }
                }

                // Add initial state to the list
                visitedStates = visitedStates.Add(currentState);

                currentState = states.Except(visitedStates).FirstOrDefault();
            } while (currentState != null);

            var finalState = new State
            {
                Id = states.Count,
                Actions = ImmutableList<IAction>.Empty
                    .Add(new Accept {Terminal = _pipelinePayload.AugmentedGrammar.Eof.Value}),
                Rules = ImmutableDictionary<IProduction, int>.Empty
                    .Add(rootProduction, 1)
            };

            initialState.Actions = initialState.Actions
                .Add(new Shift {Terminal = _pipelinePayload.AugmentedGrammar.Eof.Value});

            states = states.Add(finalState);

            return states;
        }
    }
}