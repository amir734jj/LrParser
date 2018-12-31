using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models.Interfaces;

namespace Core.Models
{
    public class SlrParserPayload : ISlrParserPayload
    {
        public IGrammar Grammar { get; set; }
        
        public ITerminal Eof { get; set; }
   
        public ImmutableList<ITerminal> Terminals { get; set; }
        
        public ImmutableList<INonTerminal> NonTerminals { get; set; }
    }
}