using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Interfaces.Actions
{
    public interface IShift : IAction
    {
        ITerminal Terminal { get; set; }
        
        IState Destination { get; set; }
    }
}