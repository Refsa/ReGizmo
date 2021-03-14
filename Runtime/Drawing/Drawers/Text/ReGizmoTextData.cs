using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct CharacterInfoShader
    {
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector4 Size;
        public float Advance;

        public override string ToString()
        {
            return $"{Size.ToString()}, {Advance.ToString()}";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct CharData
    {
        public uint TextID;
        public uint CharIndex;
        public float Advance;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TextData
    {
        public Vector3 Color;
        public float Scale;
        public Vector3 Position;
        public float CenterOffset;
    }
}