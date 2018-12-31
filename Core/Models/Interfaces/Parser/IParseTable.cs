using System.Collections.Immutable;

namespace Core.Models.Interfaces
{
    public interface IParseTable
    {
        ImmutableDictionary<IState, ImmutableDictionary<ITerminalOrNonTerminal, IAction>> StateMap { get; set; }
    }
}