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
    internal class DrawSDFText : DrawBase
    {
        [SerializeField] string text = "Hello";
        [SerializeField, Range(8f, 1024f)] float fontSize = 1f;

        protected override void Draw()
        {
            using (new TransformScope(transform))
            {
                ReDraw.TextSDF("SDF: " + text, Vector3.zero, fontSize, Color.white);
                ReDraw.Text("Regular: " + text, Vector3.up * 5, fontSize, Color.white);
            }
        }

        protected override void DrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Draw();
            }

#if UNITY_EDITOR
            Handles.Label(transform.position + Vector3.down * 5f, text);
#endif
        }
    }
}