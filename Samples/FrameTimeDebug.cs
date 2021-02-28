using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class FrameTimeDebug : MonoBehaviour
{
    Stopwatch totalFrameTimeSW = new Stopwatch();
    Queue<long> avgFrameTime = new Queue<long>();

    void Update()
    {
        if (Time.frameCount < 100) return;

        totalFrameTimeSW.Stop();
        avgFrameTime.Enqueue(totalFrameTimeSW.ElapsedTicks);
        if (avgFrameTime.Count > 1000)
        {
            avgFrameTime.Dequeue();
        }
        totalFrameTimeSW.Restart();
    }

    void OnGUI()
    {
        if (avgFrameTime.Count == 0) return;

        float frameTimeAverage = (float)avgFrameTime.Average() / 10_000f;
        float avgFps = 1000f / frameTimeAverage;

        Rect rect = new Rect(0, 0, 150, 50);

        using (new GUILayout.AreaScope(rect))
        {
            GUILayout.Label($"{avgFps:F1}");
        }
    }
}
