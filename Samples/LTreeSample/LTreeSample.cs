using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class LTreeSample : MonoBehaviour
    {
        struct State
        {
            public Vector3 Pos;
            public Vector3 Dir;
            public Mode Mode;

            public State(Vector3 pos, Vector3 dir, Mode mode)
            {
                Pos = pos;
                Dir = dir;
                Mode = mode;
            }
        }

        enum Mode
        {
            Unknown = 0,
            Expand,
            DrawForward,
            TurnRight,
            TurnLeft,
            Save,
            Load,
        }

        delegate LinkedListNode<Mode> ExpandRuleDelegate(LL<Mode> ltree, LinkedListNode<Mode> root);
        delegate LinkedListNode<Mode> GrowRuleDelegate(LL<Mode> ltree, LinkedListNode<Mode> root);

        [SerializeField] bool rebuild = false;

        Stack<State> states = new Stack<State>();
        LL<Mode> ltree = new LL<Mode>();
        LL<State> generated = new LL<State>();

        ILTree _ltree;

        void OnDrawGizmos()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            if (rebuild)
            {
                _ltree = new FernLTree();
                _ltree.Generate(6);
                _ltree.Build();

                /* ltree.Clear();
                generated = BuildFern(ltree); */
                rebuild = false;
            }

            if (_ltree == null) return;
            _ltree.Draw();

            /* if (generated.First == null || generated.First.Next == null) return;
            var prevState = generated.First.Value;
            var currNode = generated.First.Next;
            states.Clear();

            while (currNode != null)
            {
                var state = currNode.Value;
                switch (state.Mode)
                {
                    case Mode.DrawForward:
                        ReDraw.Line(prevState.Pos, state.Pos, Color.green, 1f);
                        // ReDraw.Triangle(state.Pos, state.Dir, DrawMode.BillboardAligned, Size.Units(1f), 0f, Color.green);
                        break;
                    case Mode.Save:
                        states.Push(state);
                        break;
                    case Mode.Load:
                        var mode = state.Mode;
                        state = states.Pop();
                        state.Mode = mode;
                        break;
                }

                prevState = state;
                currNode = currNode.Next;
            } */

            sw.Stop();
            // Debug.Log($"elapsed {sw.ElapsedTicks / 10_000f} ms");
        }

        static LL<State> BuildFern(LL<Mode> ltree)
        {
            ltree.AddFirst(Mode.Expand);
            return Build(ltree, FernExpandRule, FernGrowRule);
        }

        static LinkedListNode<Mode> FernExpandRule(LL<Mode> ltree, LinkedListNode<Mode> expand)
        {
            var current = ltree.AddAfter(expand, Mode.DrawForward);
            current = ltree.AddAfter(current, Mode.TurnLeft);
            current = ltree.AddAfter(current, Mode.Save);
            current = ltree.AddAfter(current, Mode.Save);
            current = ltree.AddAfter(current, Mode.Expand);
            current = ltree.AddAfter(current, Mode.Load);
            current = ltree.AddAfter(current, Mode.TurnRight);
            current = ltree.AddAfter(current, Mode.Expand);
            current = ltree.AddAfter(current, Mode.Load);
            current = ltree.AddAfter(current, Mode.TurnRight);
            current = ltree.AddAfter(current, Mode.DrawForward);
            current = ltree.AddAfter(current, Mode.Save);
            current = ltree.AddAfter(current, Mode.TurnRight);
            current = ltree.AddAfter(current, Mode.DrawForward);
            current = ltree.AddAfter(current, Mode.Expand);
            current = ltree.AddAfter(current, Mode.Load);
            current = ltree.AddAfter(current, Mode.TurnLeft);
            current = ltree.AddAfter(current, Mode.Expand);
            return current;
        }

        static LinkedListNode<Mode> FernGrowRule(LL<Mode> ltree, LinkedListNode<Mode> expand)
        {
            var current = ltree.AddAfter(expand, Mode.DrawForward);
            current = ltree.AddAfter(current, Mode.DrawForward);
            return current;
        }

        static LL<State> Build(LL<Mode> ltree, ExpandRuleDelegate expandRule, GrowRuleDelegate growRule)
        {
            for (int i = 0; i < 6; i++)
            {
                var expand = ltree.Find(Mode.Expand);
                while (expand != null)
                {
                    var current = expandRule.Invoke(ltree, expand);

                    ltree.Remove(expand);
                    expand = ltree.FindAfter(current, m => m == Mode.Expand);
                }

                expand = ltree.Find(Mode.DrawForward);
                while (expand != null)
                {
                    var current = growRule.Invoke(ltree, expand);

                    ltree.Remove(expand);
                    expand = ltree.FindAfter(current, m => m == Mode.DrawForward);
                }
            }
            return Generate(ltree);
        }

        static LL<State> Generate(LL<Mode> ltree)
        {
            LL<State> generated = new LL<State>();
            Stack<State> states = new Stack<State>();

            State prevState = new State(Vector3.zero, Vector3.up, Mode.Unknown);
            var genNode = ltree.First;
            while (genNode != null)
            {
                State state = new State(prevState.Pos, prevState.Dir, genNode.Value);

                switch (genNode.Value)
                {
                    case Mode.Expand:
                        break;
                    case Mode.DrawForward:
                        state.Pos += state.Dir;
                        break;
                    case Mode.TurnRight:
                        state.Dir = Quaternion.Euler(-Random.Range(0f, 40f), 0f, -Random.Range(0f, 40f)) * state.Dir;
                        break;
                    case Mode.TurnLeft:
                        state.Dir = Quaternion.Euler(Random.Range(0f, 40f), 0f, Random.Range(0f, 40f)) * state.Dir;
                        break;
                    case Mode.Save:
                        states.Push(state);
                        break;
                    case Mode.Load:
                        var mode = state.Mode;
                        state = states.Pop();
                        state.Mode = mode;
                        break;
                }

                generated.AddLast(state);
                prevState = state;
                genNode = genNode.Next;
            }

            return generated;
        }
    }
}