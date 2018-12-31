namespace Core.Models.Interfaces
{
    public interface ITerminal : ITerminalOrNonTerminal
    {
        System.Enum Name { get; set; }
    }
}