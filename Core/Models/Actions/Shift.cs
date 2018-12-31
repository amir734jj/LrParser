using Core.Models.Interfaces;

namespace Core.Models.Actions
{
    public class Shift : IShift
    {
        public ITerminalOrNonTerminal TerminalOrNonTerminal { get; set; }
        
        public IState Destination { get; set; }
    }
}