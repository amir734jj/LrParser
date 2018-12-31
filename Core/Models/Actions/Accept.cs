using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Actions
{
    public class Accept : IAccept
    {
        public ITerminal Terminal { get; set; }
    }
}