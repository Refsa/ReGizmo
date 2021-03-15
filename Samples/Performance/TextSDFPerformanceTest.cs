using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class TextSDFPerformanceTest : PerformanceTest
{
    const string text = "Hello";
    
    protected override void RunInternal()
    {
        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                ReDraw.TextSDF(text, new Vector3(x, 0, y) * 2, 1f, Color.green);
            }
        }
    }
}