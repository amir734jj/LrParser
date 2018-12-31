using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models.Interfaces;

namespace Core.Models
{
    public class State : IState
    {
        public ImmutableDictionary<IProduction, int> Rules { get; set; }
        
        public Dictionary<ITerminalOrNonTerminal, IAction> Actions { get; set; }
    }
}