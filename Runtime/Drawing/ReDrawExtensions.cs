using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color1;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
            }
        } 

        public static void TextSDF(string text, UnityEngine.Vector3 position, System.Single scale,
            UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoSDFFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition + position;
                textData.Scale = scale;
                textData.Color = new Vector3(color.r, color.g, color.b);

                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint) text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
    }
}