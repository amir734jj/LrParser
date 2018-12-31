using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Parser
{
    public class ParseTable : IParseTable
    {
        public ImmutableDictionary<IState, ImmutableDictionary<ITerminalOrNonTerminal, IAction>> StateMap { get; set; }
    }
}