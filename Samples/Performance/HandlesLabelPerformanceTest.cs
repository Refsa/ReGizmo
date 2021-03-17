using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class HandlesLabelPerformanceTest : MonoBehaviour
    {
        const string text = "Hello";

        void OnDrawGizmosSelected()
        {
            using (new Handles.DrawingScope(Color.blue))
            {
                for (int x = 0; x < 64; x++)
                {
                    for (int y = 0; y < 64; y++)
                    { 
                        Handles.Label(new Vector3(x, 0, y), text);
                    }
                }
            }
        }
    }
}