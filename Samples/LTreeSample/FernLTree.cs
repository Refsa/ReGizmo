using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.FernLSys
{
    public struct FernState : IState<FernMode>
    {
        public Vector3 Pos;
        public Vector3 Dir;
        public FernMode Mode { get; set; }

        public FernState(Vector3 pos, Vector3 dir, FernMode mode)
        {
            Pos = pos;
            Dir = dir;
            Mode = mode;
        }


        public IState<FernMode> Draw(IState<FernMode> prevState, Stack<IState<FernMode>> states)
        {
            IState<FernMode> nextState = this;

            switch (Mode)
            {
                case FernMode.DrawForward:
                    var ps = (FernState)prevState;
                    ReDraw.Line(ps.Pos, Pos, Color.green, 1f);
                    break;
                case FernMode.Save:
                    states.Push(this);
                    break;
                case FernMode.Load:
                    var mode = Mode;
                    nextState = states.Pop();
                    nextState.Mode = mode;
                    break;
            }

            return nextState;
        }
    }

    public enum FernMode
    {
        Unknown = 0,
        Expand,
        DrawForward,
        TurnRight,
        TurnLeft,
        Save,
        Load,
    }

    struct ExpandRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.Expand;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            return current;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            current = ltree.AddAfter(current, FernMode.DrawForward);
            current = ltree.AddAfter(current, FernMode.TurnLeft);
            current = ltree.AddAfter(current, FernMode.Save);
            current = ltree.AddAfter(current, FernMode.Save);
            current = ltree.AddAfter(current, FernMode.Expand);
            current = ltree.AddAfter(current, FernMode.Load);
            current = ltree.AddAfter(current, FernMode.TurnRight);
            current = ltree.AddAfter(current, FernMode.Expand);
            current = ltree.AddAfter(current, FernMode.Load);
            current = ltree.AddAfter(current, FernMode.TurnRight);
            current = ltree.AddAfter(current, FernMode.DrawForward);
            current = ltree.AddAfter(current, FernMode.Save);
            current = ltree.AddAfter(current, FernMode.TurnRight);
            current = ltree.AddAfter(current, FernMode.DrawForward);
            current = ltree.AddAfter(current, FernMode.Expand);
            current = ltree.AddAfter(current, FernMode.Load);
            current = ltree.AddAfter(current, FernMode.TurnLeft);
            current = ltree.AddAfter(current, FernMode.Expand);
            return current;
        }
    }

    struct DrawForwardRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.DrawForward;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            current.Pos += current.Dir;
            return current;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            current = ltree.AddAfter(current, FernMode.DrawForward);
            current = ltree.AddAfter(current, FernMode.DrawForward);
            return current;
        }
    }

    struct TurnRightRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.TurnRight;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            current.Dir = Quaternion.Euler(-Random.Range(15f, 30f), 0f, -Random.Range(15f, 30f)) * current.Dir;
            return current;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            return current;
        }
    }

    struct TurnLeftRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.TurnLeft;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            current.Dir = Quaternion.Euler(Random.Range(15f, 30f), 0f, Random.Range(15f, 30f)) * current.Dir;
            return current;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            return current;
        }
    }

    struct SaveRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.Save;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            stack.Push(current);
            return current;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            return current;
        }
    }

    struct LoadRule : IRule<FernState, FernMode>
    {
        public FernMode RuleFor => FernMode.Load;

        public FernState Build(FernState current, Stack<FernState> stack)
        {
            var state = stack.Pop();
            state.Mode = current.Mode;
            return state;
        }

        public LinkedListNode<FernMode> Generate(LL<FernMode> ltree, LinkedListNode<FernMode> current)
        {
            return current;
        }
    }

    public class FernLTree : LTree<FernState, FernMode>
    {
        protected override System.Func<FernState, FernMode, FernState> stateFactory => StateFactory;
        protected override FernState rootState => new FernState(Vector3.zero, Vector3.up, FernMode.Unknown);

        public FernLTree() : base()
        {
            rules.Add(FernMode.Expand, new ExpandRule());
            rules.Add(FernMode.DrawForward, new DrawForwardRule());
            rules.Add(FernMode.TurnRight, new TurnRightRule());
            rules.Add(FernMode.TurnLeft, new TurnLeftRule());
            rules.Add(FernMode.Save, new SaveRule());
            rules.Add(FernMode.Load, new LoadRule());
        }

        FernState StateFactory(FernState state, FernMode mode)
        {
            return new FernState(state.Pos, state.Dir, mode);
        }

        protected override void PreGenerate()
        {
            ltree.AddFirst(FernMode.Expand);
        }
    }
}