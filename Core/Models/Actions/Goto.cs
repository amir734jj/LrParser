using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Actions
{
    public class Goto : IGoto
    {
        public IState Destination { get; set; }
        
        public INonTerminal NonTerminal { get; set; }
    }
}