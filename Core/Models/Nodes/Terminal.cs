using Core.Models.Interfaces;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Nodes
{
    public class Terminal : ITerminal
    {
        public System.Enum Name { get; set; }
        
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}