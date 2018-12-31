namespace Core.Models.Interfaces
{
    public interface IGoto : IAction
    {
        IState Destination { get; set; }
        
        INonTerminal NonTerminal { get; set; }
    }
}