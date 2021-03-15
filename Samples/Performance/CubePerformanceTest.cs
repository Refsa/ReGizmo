using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class CubePerformanceTest : PerformanceTest
{
    protected override void RunInternal()
    {
        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                ReDraw.Cube(new Vector3(x, 0, y) * 2, Color.blue);
            }
        }
    }
}