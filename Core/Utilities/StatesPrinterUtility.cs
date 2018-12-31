using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Interfaces;
using Core.Models.Interfaces.Parser;

namespace Core.Utilities
{
    public static class StatesPrinterUtility
    {
        public static string Print(this IEnumerable<IState> states)
        {
            return string.Join(Environment.NewLine, states.OrderBy(x => x.Id));
        }
    }
}