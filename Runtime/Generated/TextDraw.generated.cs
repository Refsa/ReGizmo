
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Text(string text, UnityEngine.Vector3 position, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition + position;
                textData.Scale = scale;
                textData.Color = new Vector3(color.r, color.g, color.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition + position;
                textData.Scale = scale;
                textData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition + position;
                textData.Scale = 1f;
                textData.Color = new Vector3(color.r, color.g, color.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = scale;
                textData.Color = new Vector3(color.r, color.g, color.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition + position;
                textData.Scale = 1f;
                textData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = scale;
                textData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = 1f;
                textData.Color = new Vector3(color.r, color.g, color.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = 1f;
                textData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
    }
}