namespace Core.Models.Interfaces.Nodes
{
    public interface INonTerminal : ITerminalOrNonTerminal
    {
        System.Enum Name { get; set; }
    }
}