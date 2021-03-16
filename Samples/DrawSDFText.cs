using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace ReGizmo.Samples
{
    public class DrawSDFText : DrawBase
    {
        [SerializeField] string text = "Hello";
        [SerializeField, Range(1f, 32f)] float fontSize = 1f;

        protected override void Draw()
        {
            ReDraw.TextSDF("SDF: " + text, Vector3.left * 0.15f, fontSize, Color.green);
            ReDraw.Text("REG: " + text, Vector3.right * 0.15f, fontSize, Color.green);
        }

        protected override void DrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Draw();
            }

            Handles.Label(Vector3.left * 0.15f, text);
            Handles.Label(Vector3.right * 0.15f, text);
        }
    }
}