using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace ReGizmo.Samples
{
    [AddComponentMenu("ReGizmo Samples/Draw SDF Text")]
    public class DrawSDFText : DrawBase
    {
        [SerializeField] string text = "Hello";
        [SerializeField, Range(8f, 128f)] float fontSize = 1f;

        protected override void Draw()
        {
            using (new TransformScope(transform))
            {
                ReDraw.TextSDF("SDF: " + text, Vector3.zero, fontSize, Color.green);
                ReDraw.Text("Regular: " + text, Vector3.up * 5, fontSize, Color.blue);
            }
        }

        protected override void DrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Draw();
            }

#if UNITY_EDITOR
            Handles.Label(Vector3.left * 0.15f, text);
            Handles.Label(Vector3.right * 0.15f, text);
#endif
        }
    }
}