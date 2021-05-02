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
        [SerializeField, Range(1f, 32f)] float fontSize = 1f;

        protected override void Draw()
        {
            ReDraw.TextSDF("SDF", Vector3.left * 0.15f + Vector3.up * 0.15f, 2f, Color.green);
            ReDraw.TextSDF(text, Vector3.left * 0.15f, fontSize, Color.green);

            ReDraw.TextSDF("Regular", Vector3.right * 0.15f + Vector3.up * 0.15f, 2f, Color.blue);
            ReDraw.Text(text, Vector3.right * 0.15f, fontSize, Color.blue);
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