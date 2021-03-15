﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class FrameTimeDebug : MonoBehaviour
{
    Stopwatch totalFrameTimeSW = new Stopwatch();
    Queue<long> avgFrameTime = new Queue<long>();

    public float LastAvgFrameTime { get; private set; }

    void Update()
    {
        if (Time.frameCount < 100) return;

        totalFrameTimeSW.Stop();
        avgFrameTime.Enqueue(totalFrameTimeSW.ElapsedTicks);
        if (avgFrameTime.Count > 1000)
        {
            avgFrameTime.Dequeue();
        }

        if (avgFrameTime.Count > 0)
        {
            LastAvgFrameTime = 1000f / ((float) avgFrameTime.Average() / 10_000f);
        }

        totalFrameTimeSW.Restart();
    }

    public void Clear()
    {
        avgFrameTime.Clear();
    }

    void OnGUI()
    {
        Rect rect = new Rect(0, 0, 150, 50);

        using (new GUILayout.AreaScope(rect))
        {
            GUILayout.Label($"{LastAvgFrameTime:F1}");
        }
    }
}