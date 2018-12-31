using System;
using System.Collections.Immutable;
using Core.Models.Grammar;
using Core.Models.Interfaces;

namespace Core.Builders
{
    public class GrammarBuilder
    {
        private readonly IGrammar _grammar;

        public static GrammarBuilder New()
        {
            return new GrammarBuilder();
        }

        public GrammarBuilder()
        {
            _grammar = new Grammar
            {
                Productions = ImmutableList<IProduction>.Empty
            };
        }
       

        public static  GrammarBuilder()
        {
            return Func<IProduction>
        }
    }
}