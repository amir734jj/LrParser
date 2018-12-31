using System;
using System.Collections.Immutable;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Parser
{
    public class SlrParserPayload
    {
        public IGrammar Grammar { get; set; }
        
        public Func<ImmutableArray<ITerminal>, ImmutableList<ITerminal>> Lexer { get; set; }
    }
}