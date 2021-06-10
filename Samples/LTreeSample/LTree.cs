using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class LL<T> : System.Collections.Generic.LinkedList<T>
    {
        public LinkedListNode<T> FindAfter(LinkedListNode<T> current, System.Func<T, bool> predicate)
        {
            if (current.Next == null) return null;

            return FindAfterInternal(current.Next, predicate);
        }

        LinkedListNode<T> FindAfterInternal(LinkedListNode<T> current, System.Func<T, bool> predicate)
        {
            if (predicate.Invoke(current.Value))
            {
                return current;
            }

            if (current.Next == null) return null;
            return FindAfterInternal(current.Next, predicate);
        }
    }

    public interface IState<TMode>
        where TMode : Enum
    {
        TMode Mode { get; set; }

        IState<TMode> Draw(IState<TMode> prevState, Stack<IState<TMode>> states);
    }

    public interface IRule<TState, TMode>
        where TState : struct, IState<TMode>
        where TMode : Enum
    {
        TMode RuleFor { get; }
        LinkedListNode<TMode> Generate(LL<TMode> ltree, LinkedListNode<TMode> current);
        TState Build(TState current, Stack<TState> stack);
    }

    public interface ILTree
    {
        void Generate(int generations);
        void Build();
        void Draw();
    }

    public abstract class LTree<TState, TMode> : ILTree
        where TState : struct, IState<TMode>
        where TMode : Enum
    {
        protected Dictionary<TMode, IRule<TState, TMode>> rules;
        protected LL<TMode> ltree;

        protected LL<TState> generated;
        protected Stack<TState> buildStates;
        protected Stack<IState<TMode>> drawStates;

        protected abstract System.Func<TState, TMode, TState> stateFactory { get; }
        protected abstract TState rootState { get; }

        public LTree()
        {
            rules = new Dictionary<TMode, IRule<TState, TMode>>();
            ltree = new LL<TMode>();
            generated = new LL<TState>();
            buildStates = new Stack<TState>();
            drawStates = new Stack<IState<TMode>>();
        }

        public void Generate(int generations)
        {
            ltree.Clear();
            PreGenerate();

            for (int i = 0; i < generations; i++)
            {
                foreach (var rule in rules.Values)
                {
                    var current = ltree.Find(rule.RuleFor);
                    while (current != null)
                    {
                        var next = rule.Generate(ltree, current);

                        ltree.Remove(current);
                        current = ltree.FindAfter(next, m => m.Equals(rule.RuleFor));
                    }
                }
            }
        }

        protected abstract void PreGenerate();

        public void Build()
        {
            generated.Clear();
            buildStates.Clear();

            TState prevState = rootState;
            var current = ltree.First;

            while (current != null)
            {
                TState currState = stateFactory.Invoke(prevState, current.Value);

                if (rules.TryGetValue(current.Value, out var rule))
                {
                    currState = rule.Build(currState, buildStates);

                    generated.AddLast(currState);
                    prevState = currState;
                }

                current = current.Next;
            }
        }

        public void Draw()
        {
            if (generated.First == null || generated.First.Next == null) return;

            var prevState = generated.First.Value;
            var currNode = generated.First.Next;

            while (currNode != null)
            {
                var state = currNode.Value;
                prevState = (TState)state.Draw((IState<TMode>)prevState, drawStates);

                currNode = currNode.Next;
            }
        }
    }
}
