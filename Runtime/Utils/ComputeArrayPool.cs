using System.Collections.Generic;

namespace ReGizmo.Utils
{
    internal static class ComputeArrayPool<TElement>
        where TElement : unmanaged
    {
        const int InitialSize = 64;

        static Queue<ComputeArray<TElement>> openPool = new Queue<ComputeArray<TElement>>();

        static ComputeArrayPool()
        {
            for (int i = 0; i < InitialSize; i++)
            {
                openPool.Enqueue(new ComputeArray<TElement>());
            }
        }

        internal static ComputeArray<TElement> Get()
        {
            ComputeArray<TElement> target = null;
            if (openPool.Count == 0)
            {
                target = new ComputeArray<TElement>();
            }
            else
            {
                target = openPool.Dequeue();
            }

            return target;
        }

        internal static void Release(ComputeArray<TElement> computeArray)
        {
            computeArray.Clear();

            openPool.Enqueue(computeArray);
        }
    }
}