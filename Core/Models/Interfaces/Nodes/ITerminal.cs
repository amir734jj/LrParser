namespace Core.Models.Interfaces.Nodes
{
    public interface ITerminal : ITerminalOrNonTerminal
    {
        System.Enum Name { get; set; }
    }
}