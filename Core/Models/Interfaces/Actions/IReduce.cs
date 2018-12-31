using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Interfaces.Actions
{
    public interface IReduce : IAction
    {
        IProduction Production { get; set; }
        
        INonTerminal NonTerminal { get; set; }
    }
}