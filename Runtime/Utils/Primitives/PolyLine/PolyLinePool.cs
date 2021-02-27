using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal static class PolyLinePool
    {
        const int InitialPoolSize = 100;

        static Queue<List<PolyLineData>> openPool = new Queue<List<PolyLineData>>();
        static HashSet<List<PolyLineData>> closedPool = new HashSet<List<PolyLineData>>();

        internal static void SetupPool()
        {
            for (int i = 0; i < InitialPoolSize; i++)
            {
                openPool.Enqueue(new List<PolyLineData>());
            }
        }

        internal static List<PolyLineData> Get()
        {
            if (openPool.Count == 0)
            {
                openPool.Enqueue(new List<PolyLineData>());
                UnityEngine.Debug.Log($"expand");
            }

            var target = openPool.Dequeue();
            closedPool.Add(target);

            return target;
        }

        internal static void Release(List<PolyLineData> target)
        {
            target.Clear();

            closedPool.Remove(target);
            openPool.Enqueue(target);
        }

        public static int OpenPoolCount => openPool.Count;
        public static int ClosedPoolCount => closedPool.Count;
    }
}