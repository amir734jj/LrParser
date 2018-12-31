using System.Collections.Immutable;
using Core.Interfaces;
using Core.Interfaces.Logic;
using Core.Models.Grammar;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Parser;
using Core.Models.Pipeline;

namespace Core.Logic
{
    public class AugmentGrammarLogic : IAugmentGrammarLogic
    {
        private readonly PipelinePayload _pipelinePayload;

        public AugmentGrammarLogic(PipelinePayload pipelinePayload)
        {
            _pipelinePayload = pipelinePayload;
        }
        
        public IGrammar AugmentGrammar()
        {
            var augmentedGrammar = new Grammar
            {
                Start = _pipelinePayload.Grammar.Start,
                Productions = _pipelinePayload.Grammar.Productions.Add(new Production
                {
                    Order = 0,
                    NonTerminal = _pipelinePayload.Grammar.Eof.Key,
                    Items = ImmutableList<ITerminalOrNonTerminal>.Empty
                        .Add(_pipelinePayload.Grammar.Start)
                        .Add(_pipelinePayload.Grammar.Eof.Value)
                        .ToImmutableArray()
                }),
                Eof = _pipelinePayload.Grammar.Eof,
                Epsilon = _pipelinePayload.Grammar.Epsilon,
                NonTerminals = _pipelinePayload.Grammar.NonTerminals,
                Terminals = _pipelinePayload.Grammar.Terminals,
                TerminalsAndNonTerminals = _pipelinePayload.Grammar.TerminalsAndNonTerminals
            };

            return augmentedGrammar;
        }
    }
}