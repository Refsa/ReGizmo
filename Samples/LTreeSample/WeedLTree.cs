using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;
namespace ReGizmo.Samples.WeedLSys
{
    public struct WeedState : IState<WeedMode>
    {
        public Vector3 Pos;
        public Vector3 Dir;
        public WeedMode Mode { get; set; }

        public WeedState(Vector3 pos, Vector3 dir, WeedMode mode)
        {
            Pos = pos;
            Dir = dir;
            Mode = mode;
        }

        public IState<WeedMode> Draw(IState<WeedMode> prevState, Stack<IState<WeedMode>> states)
        {
            IState<WeedMode> nextState = this;

            switch (Mode)
            {
                case WeedMode.GrowF:
                    var ps = (WeedState)prevState;
                    ReDraw.Line(ps.Pos, Pos, ReColors.APRICOT, 1f);
                    break;
                case WeedMode.Save:
                    states.Push(this);
                    break;
                case WeedMode.Load:
                    var mode = Mode;
                    nextState = states.Pop();
                    nextState.Mode = mode;
                    break;
            }

            return nextState;
        }
    }

    public enum WeedMode
    {
        Unknown = 0,
        GrowF,
        ExpandX,
        ExpandY,
        RotateL,
        RotateR,
        Save,
        Load,
    }

    struct GrowFRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.GrowF;

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            current = ltree.AddAfter(current, WeedMode.GrowF);
            current = ltree.AddAfter(current, WeedMode.GrowF);
            current = ltree.AddAfter(current, WeedMode.RotateL);
            current = ltree.AddAfter(current, WeedMode.Save);
            current = ltree.AddAfter(current, WeedMode.ExpandX);
            current = ltree.AddAfter(current, WeedMode.ExpandY);
            current = ltree.AddAfter(current, WeedMode.Load);
            current = ltree.AddAfter(current, WeedMode.RotateR);
            current = ltree.AddAfter(current, WeedMode.Save);
            current = ltree.AddAfter(current, WeedMode.ExpandX);
            current = ltree.AddAfter(current, WeedMode.ExpandY);
            current = ltree.AddAfter(current, WeedMode.Load);

            return current;
        }

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            current.Pos += current.Dir;
            return current;
        }
    }

    struct GrowXRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.ExpandX;

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            current = ltree.AddAfter(current, WeedMode.RotateR);
            current = ltree.AddAfter(current, WeedMode.GrowF);
            current = ltree.AddAfter(current, WeedMode.ExpandY);
            current = ltree.AddAfter(current, WeedMode.ExpandX);

            return current;
        }

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            // current.Pos += current.Dir;
            return current;
        }
    }

    struct GrowYRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.ExpandY;

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            current = ltree.AddAfter(current, WeedMode.RotateL);
            current = ltree.AddAfter(current, WeedMode.GrowF);
            current = ltree.AddAfter(current, WeedMode.ExpandX);
            current = ltree.AddAfter(current, WeedMode.ExpandY);

            return current;
        }

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            // current.Pos += current.Dir;
            return current;
        }
    }

    struct RotateLRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.RotateL;

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            return current;
        }

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            current.Dir = Quaternion.Euler(Random.Range(10f, 20f), 0f, Random.Range(10f, 20f)) * current.Dir;
            return current;
        }
    }

    struct RotateRRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.RotateR;

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            return current;
        }

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            current.Dir = Quaternion.Euler(-Random.Range(10f, 20f), 0f, -Random.Range(10f, 20f)) * current.Dir;
            return current;
        }
    }

    struct SaveRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.Save;

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            stack.Push(current);
            return current;
        }

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            return current;
        }
    }

    struct LoadRule : IRule<WeedState, WeedMode>
    {
        public WeedMode RuleFor => WeedMode.Load;

        public WeedState Build(WeedState current, Stack<WeedState> stack)
        {
            var state = stack.Pop();
            state.Mode = current.Mode;
            return state;
        }

        public LinkedListNode<WeedMode> Generate(LL<WeedMode> ltree, LinkedListNode<WeedMode> current)
        {
            return current;
        }
    }

    /// <summary>
    /// LTree config from http://paulbourke.net/fractals/lsys/
    /// </summary>
    public class WeedLTree : LTree<WeedState, WeedMode>
    {
        protected override System.Func<WeedState, WeedMode, WeedState> stateFactory => StateFactory;

        protected override WeedState rootState => new WeedState(Vector3.zero, Vector3.up, WeedMode.Unknown);

        public WeedLTree() : base()
        {
            rules.Add(WeedMode.GrowF, new GrowFRule());
            rules.Add(WeedMode.ExpandY, new GrowYRule());
            rules.Add(WeedMode.ExpandX, new GrowXRule());
            rules.Add(WeedMode.RotateL, new RotateLRule());
            rules.Add(WeedMode.RotateR, new RotateRRule());
            rules.Add(WeedMode.Save, new SaveRule());
            rules.Add(WeedMode.Load, new LoadRule());
        }

        protected override void PreGenerate()
        {
            ltree.AddFirst(WeedMode.GrowF);
        }

        WeedState StateFactory(WeedState prevState, WeedMode mode)
        {
            return new WeedState(prevState.Pos, prevState.Dir, mode);
        }
    }
}