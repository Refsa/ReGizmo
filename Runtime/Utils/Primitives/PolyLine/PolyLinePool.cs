using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo
{
    internal static class PolyLinePool
    {
        const int InitialPoolSize = 100;

        static Queue<List<LineData>> openPool = new Queue<List<LineData>>();
        static HashSet<List<LineData>> closedPool = new HashSet<List<LineData>>();

        internal static void SetupPool()
        {
            for (int i = 0; i < InitialPoolSize; i++)
            {
                openPool.Enqueue(new List<LineData>());
            }
        }

        internal static List<LineData> Get()
        {
            if (openPool.Count == 0)
            {
                openPool.Enqueue(new List<LineData>());
                UnityEngine.Debug.Log($"expand");
            }

            var target = openPool.Dequeue();
            closedPool.Add(target);

            return target;
        }

        internal static void Release(List<LineData> target)
        {
            target.Clear();

            closedPool.Remove(target);
            openPool.Enqueue(target);
        }

        public static int OpenPoolCount => openPool.Count;
        public static int ClosedPoolCount => closedPool.Count;
    }
}