
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Cube(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            if (ReGizmoResolver<ReGizmoCubeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position;
                shaderData.Color = color;
                shaderData.Scale = scale;
                shaderData.Rotation = rotation.eulerAngles * Mathf.Deg2Rad;
            }
        }

        public static void Mesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = position;
                shaderData.Color = color;
                shaderData.Scale = scale;
                shaderData.Rotation = rotation.eulerAngles * Mathf.Deg2Rad;
            }
        }

        public static void Text(string text, Vector3 position, float scale, Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = position;
                    charData.Color = new Vector3(color.r, color.g, color.b);
                    charData.Scale = scale;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = color1;
                shaderData.Width = width1;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
                shaderData.Mode = 1;
            }
        }

        public static void Icon(Texture2D texture, Vector3 position, Color color, float scale)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = position;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = scale;
            }
        }
    }
}