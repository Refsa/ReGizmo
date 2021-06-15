using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class TextSDFPerformanceTest : PerformanceTest, IParallelTest, ISequentialTest
    {
        const string text = "Hello";

        public void RunParallelTest()
        {
            Enumerable.Range(0, testSizeSqr * testSizeSqr)
                .AsParallel()
                .ForAll(val =>
                {
                    int x = val / testSizeSqr;
                    int y = val % testSizeSqr;

                    ReDraw.TextSDF(text, new Vector3(x, 0, y) * 5, 12f, Color.green);
                });
        }

        public void RunSequentialTest()
        {
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    ReDraw.TextSDF(text, new Vector3(x, 0, y) * 5, 12f, Color.green);
                }
            }
        }
    }
}