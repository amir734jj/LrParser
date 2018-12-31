using System.Collections.Immutable;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Interfaces.Parser
{
    public interface IParseTable
    {
        ImmutableDictionary<IState, ImmutableDictionary<ITerminalOrNonTerminal, IAction>> StateMap { get; set; }
    }
}