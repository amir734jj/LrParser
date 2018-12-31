using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Actions
{
    public class Reduce : IReduce
    {
        public IProduction Production { get; set; }
        
        public INonTerminal NonTerminal { get; set; }
    }
}