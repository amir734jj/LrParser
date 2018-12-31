using Core.Models.Interfaces.Nodes;

namespace Core.Models.Interfaces.Actions
{
    public interface IAccept : IAction
    {
        ITerminal Terminal { get; set; }
    }
}