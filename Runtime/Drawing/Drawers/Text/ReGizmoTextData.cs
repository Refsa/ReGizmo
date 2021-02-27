using UnityEngine;

namespace ReGizmo.Rendering
{
    struct CharacterInfoShader
    {
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector4 Size;
        public float Advance;
    }

    public struct CharData
    {
        public Vector3 Position;
        public float Scale;

        public uint CharIndex; // UNIQUE
        public Vector3 Color; // UNIQUE?
        public float Advance; // UNIQUE
    }
}