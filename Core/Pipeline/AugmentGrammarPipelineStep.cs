using System;
using Core.Logic;
using Core.Models.Pipeline;
using Tamarack.Pipeline;

namespace Core.Pipeline
{
    public class AugmentGrammarPipelineStep : IFilter<PipelinePayload>
    {
        public void Execute(PipelinePayload context, Action<PipelinePayload> executeNext)
        {
            context.AugmentedGrammar = new AugmentGrammarLogic(context).AugmentGrammar();

            executeNext(context);
        }
    }
}