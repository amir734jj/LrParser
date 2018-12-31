using System;
using System.Collections.Immutable;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;
using Core.Models.Parser;

namespace Core.Models.Pipeline
{
    public class PipelinePayload :  SlrParserPayload
    {
        public ImmutableHashSet<IState> States { get; set; }
        
        public IGrammar AugmentedGrammar { get; set; }
        
        public IParseTable ParseTable { get; set; }
        
        public TreeNode RooTreeNode { get; set; }

        public static PipelinePayload FromSlrParserPayload(SlrParserPayload slrParserPayload)
        {
            return new PipelinePayload
            {
                Grammar = slrParserPayload.Grammar,
                Lexer = slrParserPayload.Lexer
            };
        }
    }
}