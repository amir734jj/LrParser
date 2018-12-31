using System;

namespace Core.Interfaces.Builders
{
    public interface IProductionBuilderStruct<in TNonTerminal, in TTerminal>
        where TNonTerminal : Enum
        where TTerminal : Enum
    {
        Action<TNonTerminal> NonTerminal { get; }

        Action<TTerminal> Terminal { get; }

        Action<int> Order { get; }
    }
}