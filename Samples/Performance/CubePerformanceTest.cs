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
    public class CubePerformanceTest : PerformanceTest
    {
        protected override void RunInternal()
        {
            /* Enumerable.Range(0, testSizeSqr * testSizeSqr).AsParallel().ForAll(val =>
            {
                int x = val / testSizeSqr;
                int y = val % testSizeSqr;

                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            }); */

            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                int x = i / testSizeSqr;
                int y = i % testSizeSqr;

                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            }
        }
    }
}