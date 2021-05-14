﻿using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class TextPerformanceTest : PerformanceTest
    {
        const string text = "Hello";

        protected override void RunInternal()
        {
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    ReDraw.Text(text, new Vector3(x, 0, y), 12f, Color.green);
                }
            }
        }
    }
}