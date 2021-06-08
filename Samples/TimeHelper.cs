using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Samples
{
    public static class TimeHelper
    {
#if !UNITY_EDITOR
        public static float Time => UnityEngine.Time.time;
#else
        public static float Time => Application.isPlaying ? UnityEngine.Time.time : (float)UnityEditor.EditorApplication.timeSinceStartup;
#endif
    }
}