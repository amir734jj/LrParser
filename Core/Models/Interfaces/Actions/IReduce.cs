namespace Core.Models.Interfaces
{
    public interface IReduce : IAction
    {
        IProduction Production { get; set; }
        
        INonTerminal NonTerminal { get; set; }
    }
}