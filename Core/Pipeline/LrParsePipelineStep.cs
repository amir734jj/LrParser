using System;
using Core.Logic;
using Core.Models.Pipeline;
using Tamarack.Pipeline;

namespace Core.Pipeline
{
    public class LrParsePipelineStep : IFilter<PipelinePayload>
    {
        public void Execute(PipelinePayload context, Action<PipelinePayload> executeNext)
        {
            var lexer = context.Lexer(context.Grammar.Terminals);
            
            context.RooTreeNode = new LrParseLogic(context).LalrParse(lexer);

            executeNext(context);
        }
    }
}