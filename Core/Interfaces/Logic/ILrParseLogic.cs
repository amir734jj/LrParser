using System.Collections.Generic;
using Core.Models.Interfaces.Nodes;
using Core.Models.Parser;

namespace Core.Interfaces.Logic
{
    public interface ILrParseLogic
    {
        TreeNode LalrParse(IEnumerable<ITerminal> lexer);
    }
}