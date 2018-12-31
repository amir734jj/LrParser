using System;
using Core.Logic;
using Core.Models.Pipeline;
using Tamarack.Pipeline;

namespace Core.Pipeline
{
    public class ParseTablePipelineStep : IFilter<PipelinePayload>
    {
        public void Execute(PipelinePayload context, Action<PipelinePayload> executeNext)
        {
            context.ParseTable = new ParseTableLogic(context).ResolveParseTable();

            executeNext(context);
        }
    }
}