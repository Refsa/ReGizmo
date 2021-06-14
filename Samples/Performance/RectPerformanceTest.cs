using System;
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
    public class RectPerformanceTest : PerformanceTest, ISequentialTest, IParallelTest
    {
        public void RunParallelTest()
        {
            Enumerable.Range(0, testSizeSqr * testSizeSqr)
                .AsParallel()
                .ForAll(val =>
                {
                    int x = val / testSizeSqr;
                    int y = val % testSizeSqr;

                    ReDraw.Rect(new Rect(x, y, 1f, 1f), Color.yellow);
                });
        }

        public void RunSequentialTest()
        {
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    ReDraw.Rect(new Rect(x, y, 1f, 1f), Color.yellow);
                }
            }
        }

        protected override void RunInternal()
        {
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    ReDraw.Rect(new Rect(x, y, 1f, 1f), Color.yellow);
                }
            }
        }
    }
}