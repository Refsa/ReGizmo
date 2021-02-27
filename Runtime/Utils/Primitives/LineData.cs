using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo
{
    public struct LineData
    {
        public Vector3 Position;
        public Vector4 Color;
        public float Width;
        public float EdgeSmoothing;
        public Vector4 ID;

        public LineData(Vector3 position, Vector4 color, float width = 0.01f, float edgeSmoothing = 0f)
        {
            Position = position;
            Color = color;
            Width = width;
            EdgeSmoothing = edgeSmoothing;
            ID = Vector4.zero;
        }
    }
}