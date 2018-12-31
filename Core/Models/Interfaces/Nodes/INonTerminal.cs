namespace Core.Models.Interfaces
{
    public interface INonTerminal : ITerminalOrNonTerminal
    {
        System.Enum Name { get; set; }
    }
}