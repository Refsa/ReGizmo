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
    internal class DrawSDFText : DrawSampleBase
    {
        [SerializeField] string text = "Hello";
        [SerializeField] Color color = Color.white;
        [SerializeField, Range(8f, 1024f)] float fontSize = 16f;

        protected override void Draw()
        {
            using (new TransformScope(transform))
            {
                ReDraw.TextSDF("SDF: " + text, Vector3.zero, fontSize, Color.white, depthMode);
                ReDraw.Text("Regular: " + text, Vector3.up * 5, fontSize, color, depthMode);
            }
        }
    }
}