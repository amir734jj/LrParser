namespace Core.Models.Interfaces
{
    public interface IShift : IAction
    {
        ITerminal Terminal { get; set; }
        
        IState Destination { get; set; }
    }
}