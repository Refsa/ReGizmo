using UnityEngine;

namespace ReGizmo.Drawing
{
    internal struct CharacterInfoShader
    {
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector4 Size;
        public float Advance;
    }

    internal struct CharData
    {
        public Vector3 Position;
        public float Scale;

        public uint CharIndex; // UNIQUE
        public Vector3 Color; // UNIQUE?
        public float Advance; // UNIQUE
    }
}