#if REGIZMO_DEV
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ReGizmo.Samples
{
    public static class ThreadRandom
    {
        static ConcurrentQueue<Random> random;

        static ThreadRandom()
        {
            random = new ConcurrentQueue<Random>();
            var sysRng = new System.Random();

            for (int i = 0; i < 16; i++)
            {
                var rng = new Random((uint)sysRng.Next());
                random.Enqueue(rng);
            }
        }

        public static Random Borrow()
        {
            random.TryDequeue(out var rng);
            return rng;
        }

        public static void Release(in Random rng)
        {
            random.Enqueue(rng);
        }
    }
}
#endif