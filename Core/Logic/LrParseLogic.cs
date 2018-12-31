using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Interfaces.Logic;
using Core.Models.Interfaces.Actions;
using Core.Models.Interfaces.Nodes;
using Core.Models.Interfaces.Parser;
using Core.Models.Parser;
using Core.Models.Pipeline;

namespace Core.Logic
{
    public class LrParseLogic : ILrParseLogic
    {
        private readonly PipelinePayload _pipelinePayload;

        public LrParseLogic(PipelinePayload pipelinePayload)
        {
            _pipelinePayload = pipelinePayload;
        }

        public TreeNode LalrParse(IEnumerable<ITerminal> lexer)
        {
            var table = _pipelinePayload.ParseTable;
            var currentState = table.StateMap.Keys.First(x => x.Id == 0);

            var stack = ImmutableStack<Tuple<TreeNode, IState>>.Empty;

            // Push the start state onto the stack
            stack = stack.Push(new Tuple<TreeNode, IState>(
                new TreeNode
                {
                    Node = _pipelinePayload.AugmentedGrammar.Start,
                    Children = ImmutableList<TreeNode>.Empty
                }, currentState
            ));

            foreach (var terminal in lexer)
            {
                // Reduce any number of times for a given token. Always advance to the next token for no reduction.
                bool reduced;
                do
                {
                    reduced = false;

                    // ReSharper disable once InvertIf
                    if (table.StateMap[currentState].TryGetValue(terminal, out var action))
                    {
                        switch (action)
                        {
                            case IGoto _:
                                break;
                            case IReduce reduce:
                                // Reduce by rule N
                                var rule = reduce.Production;
                                var reduceLhs = reduce.NonTerminal;

                                // Now create an array for the symbols:
                                var treeNode = new TreeNode
                                {
                                    Node = reduceLhs
                                };

                                var list = new TreeNode[rule.Items.Length];
                                
                                // Pop the thing off the stack
                                for (var index = rule.Items.Length - 1; index >= 0; index--)
                                {
                                    stack = stack.Pop(out var node);
                                    list[index] = node.Item1;
                                }

                                treeNode.Children = list.ToImmutableList();

                                // Get the state at the top of the stack
                                var topState = stack.Peek().Item2;

                                // Get the next transition key based on the item we're reducing by
                                // It should exist in the goto table, we should never try to reduce when it doesn't make sense.
                                var newState = ((IGoto) table.StateMap[topState][reduceLhs]).Destination;

                                // Push that onto the stack
                                stack = stack.Push(new Tuple<TreeNode, IState>(treeNode, newState));

                                // Transition to the top state
                                currentState = newState;

                                // Keep reducing before moving to the next token
                                reduced = true;
                                break;
                            case IShift shift:
                                currentState = shift.Destination;
                                stack = stack.Push(new Tuple<TreeNode, IState>(new TreeNode {Node = terminal}, currentState));
                                break;
                            case IAccept _:
                                stack.Pop(out var item);
                                return item.Item1;
                        }
                    }
                } while (reduced);
            }

            return null;
        }
    }
}