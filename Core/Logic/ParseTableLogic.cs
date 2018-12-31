using System.Collections.Immutable;
using Core.Interfaces.Logic;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;
using Core.Models.Parser;
using Core.Models.Pipeline;

namespace Core.Logic
{
    public class ParseTableLogic : IParseTableLogic
    {
        private readonly PipelinePayload _pipelinePayload;

        public ParseTableLogic(PipelinePayload pipelinePayload)
        {
            _pipelinePayload = pipelinePayload;
        }

        public ParseTable ResolveParseTable()
        {
            var result = ImmutableDictionary<IState, ImmutableDictionary<ITerminalOrNonTerminal, IAction>>.Empty;
            
            foreach (var state in _pipelinePayload.States)
            {
                var map = ImmutableDictionary<ITerminalOrNonTerminal, IAction>.Empty;

                foreach (var action in state.Actions)
                {
                    switch (action)
                    {
                        case IGoto @goto when !map.ContainsKey(@goto.NonTerminal):
                            map = map.Add(@goto.NonTerminal, @goto);
                            break;
                        case IReduce reduce when !map.ContainsKey(reduce.NonTerminal):
                            map = map.Add(reduce.NonTerminal, reduce);
                            
                            foreach (var terminal in _pipelinePayload.AugmentedGrammar.Terminals)
                            {
                                map = map.Add(terminal, reduce);
                            }
                            break;
                        case IShift shift when !map.ContainsKey(shift.Terminal):
                            map = map.Add(shift.Terminal, shift);
                            break;
                        case IAccept accept when  !map.ContainsKey(accept.Terminal):
                            map = map.Add(accept.Terminal, accept);
                            break;
                    }
                }

                result = result.Add(state, map);
            }

            return new ParseTable
            {
                StateMap = result
            };
        }
    }
}