using System.Collections.Immutable;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Nodes;

namespace Core.Models.Parser
{
    public class TreeNode
    {
        public ITerminalOrNonTerminal Node { get; set; }
        
        public ImmutableList<TreeNode> Children { get; set; }
    }
}