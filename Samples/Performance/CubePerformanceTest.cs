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
    public class CubePerformanceTest : PerformanceTest, ISequentialTest, IParallelTest
    {
        public void RunParallelTest()
        {
            Enumerable.Range(0, testSizeSqr * testSizeSqr).AsParallel().ForAll(val =>
            {
                int x = val / testSizeSqr;
                int y = val % testSizeSqr;

                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            });
        }

        public void RunSequentialTest()
        {
            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                int x = i / testSizeSqr;
                int y = i % testSizeSqr;

                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            }
        }

        protected override void RunInternal()
        {
            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                int x = i / testSizeSqr;
                int y = i % testSizeSqr;

                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            }
        }
    }
}