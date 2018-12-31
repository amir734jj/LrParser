using System;
using Core.Logic;
using Core.Models.Pipeline;
using Tamarack.Pipeline;

namespace Core.Pipeline
{
    public class ParseStatePipelineStep : IFilter<PipelinePayload>
    {
        public void Execute(PipelinePayload context, Action<PipelinePayload> executeNext)
        {
            context.States = new ParseStateLogic(context).ResolveStates();

            executeNext(context);
        }
    }
}