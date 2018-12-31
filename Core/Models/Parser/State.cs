using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Grammar;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;

namespace Core.Models.Parser
{
    public class State : IState
    {
        public int Id { get; set; }

        public ImmutableDictionary<IProduction, int> Rules { get; set; }

        public ImmutableList<IAction> Actions { get; set; }

        public override string ToString()
        {
            var str = string.Empty;

            str += $"State #{Id}:" + Environment.NewLine;

            IEnumerable<string> HandleMarker(ImmutableArray<ITerminalOrNonTerminal> items, int marker)
            {
                var result = ImmutableList<string>.Empty;
                var inserted = false;
                
                for (var index = 0; index < items.Length; index++)
                {
                    if (marker <= index && !inserted)
                    {
                        result = result.Add(" . ");
                        inserted = true;
                    }

                    result = result.Add(items[index].ToString());
                }

                if (!inserted)
                {
                    result = result.Add(" . ");
                }

                return result;
            }

            foreach (var (key, value) in Rules.OrderByDescending(x => x.Value))
            {
                var items = HandleMarker(key.Items, value);

                str += $"{key.NonTerminal,8}{"->",4}{string.Join(' ', items),20}{$"({key.Order})",8}" +
                       Environment.NewLine;
            }

            foreach (var action in Actions)
            {
                switch (action)
                {
                    case IShift shift:
                        str += $"SHIFT  {shift.Terminal} and GOTO #{shift.Destination.Id}" + Environment.NewLine;
                        break;
                    case IReduce reduce:
                        str += $"REDUCE {reduce.Production.NonTerminal}" + Environment.NewLine;
                        break;
                    case IGoto @goto:
                        str += $"GOTO  #{@goto.Destination.Id} via {@goto.NonTerminal}" + Environment.NewLine;
                        break;
                    case IAccept accept:
                        str += $"ACCEPT via {accept.Terminal}" + Environment.NewLine;
                        break;
                }
            }

            return str;
        }
    }
}