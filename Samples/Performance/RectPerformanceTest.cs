using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class RectPerformanceTest : PerformanceTest
    {
        protected override void RunInternal()
        {
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    ReDraw.Rect(new Rect(x, y, 1f, 1f), Color.yellow);
                }
            }
        }
    }
}