using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public struct PolyLineData
    {
        public Vector3 Position;
        public Vector4 Color;
        public float Width;
        public int ID;

        public PolyLineData(Vector3 position, Vector4 color, float width = 1f, int id = 0)
        {
            Position = position;
            Color = color;
            Width = width;
            ID = id;
        }
    }
}