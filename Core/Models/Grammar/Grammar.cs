using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models.Interfaces;

namespace Core.Models
{
    public class Grammar : IGrammar
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ImmutableList<IProduction> Productions { get; set; }
    }
}