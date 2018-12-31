using Core.Interfaces;
using Core.Logic;
using Core.Models.Parser;
using Core.Models.Pipeline;
using Core.Pipeline;
using Tamarack.Pipeline;

namespace Core
{
    public class Parser : ISlrParser
    {
        public PipelinePayload Resolve(SlrParserPayload pipelinePayload)
        {
            var payload = PipelinePayload.FromSlrParserPayload(pipelinePayload);

            var pipeline = new Pipeline<PipelinePayload>()
                .Add<AugmentGrammarPipelineStep>()
                .Add<ParseStatePipelineStep>()
                .Add<ParseTablePipelineStep>()
                .Add<LrParsePipelineStep>();

            pipeline.Execute(payload);

            return payload;
        }
    }
}