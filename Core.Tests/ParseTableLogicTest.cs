using System.Collections.Immutable;
using System.Linq;
using Core.Builders;
using Core.Models.Interfaces.Nodes;
using Core.Models.Nodes;
using Core.Models.Parser;
using Core.Tests.Enums;
using Core.Utilities;
using Xunit;

namespace Core.Tests
{
    public class ParseTableLogicTest
    {
        private readonly Parser _logic;

        public ParseTableLogicTest()
        {
            _logic = new Parser();
        }

        [Fact]
        public void Test__NumericGrammar()
        {
            // Arrange
            var grammar = GrammarBuilder.New<NumericEnums.NonTerminals, NumericEnums.Terminals>()
                .Init()
                .WithStartSymbol(NumericEnums.NonTerminals.E)
                .WithEpsilon(NumericEnums.Terminals.Epsilon)
                .WithEof(NumericEnums.NonTerminals.Root, NumericEnums.Terminals.Eof)
                .WithProduction(NumericEnums.NonTerminals.E, x =>
                {
                    x.NonTerminal(NumericEnums.NonTerminals.E);
                    x.Terminal(NumericEnums.Terminals.Multiply);
                    x.NonTerminal(NumericEnums.NonTerminals.E);
                    x.Order(3);
                })
                .WithProduction(NumericEnums.NonTerminals.E, x =>
                {
                    x.NonTerminal(NumericEnums.NonTerminals.E);
                    x.Terminal(NumericEnums.Terminals.Plus);
                    x.NonTerminal(NumericEnums.NonTerminals.E);
                    x.Order(2);
                })
                .WithProduction(NumericEnums.NonTerminals.E, x =>
                {
                    x.Terminal(NumericEnums.Terminals.Number);
                    x.Order(1);
                })
                .Build();

            var payload = new SlrParserPayload
            {
                Grammar = grammar,
                Lexer = x =>
                {
                    var map = x.GroupBy(y => y.Name)
                        .ToDictionary(y => y.Key, y => y.First());

                    return ImmutableList<ITerminal>.Empty
                        .Add(map[NumericEnums.Terminals.Number])
                        .Add(map[NumericEnums.Terminals.Plus])
                        .Add(map[NumericEnums.Terminals.Number])
                        .Add(map[NumericEnums.Terminals.Multiply])
                        .Add(map[NumericEnums.Terminals.Number]);
                }
            };

            var str = grammar.ToString();

            // Act
            var result = _logic.Resolve(payload);

            var statesStr = result.States.Print();

            // Assert
            Assert.NotEmpty(result.States);
        }
    }
}