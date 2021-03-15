using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class DrawSDFText : MonoBehaviour
{
    const string text = "Hello";

    protected void RunInternal()
    {
        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                ReDraw.TextSDF(text, new Vector3(x, 0, y) * 2, 1f, Color.green);
            }
        }
    }

    void OnDrawGizmos()
    {
        RunInternal();
    }
}