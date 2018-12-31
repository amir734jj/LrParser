using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Interfaces.Actions
{
    public interface IGoto : IAction
    {
        IState Destination { get; set; }
        
        INonTerminal NonTerminal { get; set; }
    }
}