using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Actions
{
    public class Shift : IShift
    {
        public ITerminal Terminal { get; set; }
        
        public IState Destination { get; set; }
    }
}