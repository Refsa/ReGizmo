using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal static class PolyLinePool
    {
        const int InitialPoolSize = 100;

        static Queue<ComputeArray<PolyLineData>> openPool = new Queue<ComputeArray<PolyLineData>>();
        static HashSet<ComputeArray<PolyLineData>> closedPool = new HashSet<ComputeArray<PolyLineData>>();

        internal static void SetupPool()
        {
            for (int i = 0; i < InitialPoolSize; i++)
            {
                openPool.Enqueue(new ComputeArray<PolyLineData>());
            }
        }

        internal static ComputeArray<PolyLineData> Get()
        {
            if (openPool.Count == 0)
            {
                openPool.Enqueue(new ComputeArray<PolyLineData>());
                UnityEngine.Debug.Log($"expand");
            }

            var target = openPool.Dequeue();
            closedPool.Add(target);

            return target;
        }

        internal static void Release(ComputeArray<PolyLineData> target)
        {
            target.Clear();

            closedPool.Remove(target);
            openPool.Enqueue(target);
        }

        public static int OpenPoolCount => openPool.Count;
        public static int ClosedPoolCount => closedPool.Count;
    }
}