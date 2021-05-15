﻿using System;
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