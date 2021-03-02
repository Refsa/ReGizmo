
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Text(string text, UnityEngine.Vector3 position, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition + position;
                    charData.Scale = scale;
                    charData.Color = new Vector3(color.r, color.g, color.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition + position;
                    charData.Scale = scale;
                    charData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition + position;
                    charData.Scale = 1f;
                    charData.Color = new Vector3(color.r, color.g, color.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition;
                    charData.Scale = scale;
                    charData.Color = new Vector3(color.r, color.g, color.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition + position;
                    charData.Scale = 1f;
                    charData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition;
                    charData.Scale = scale;
                    charData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += scale * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition;
                    charData.Scale = 1f;
                    charData.Color = new Vector3(color.r, color.g, color.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
        public static void Text(string text)
        {
            if (ReGizmoResolver<ReGizmoFontDrawer>.TryGet(out var drawer))
            {
                int textLength = text.Length;
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref drawer.GetShaderData();

                    charData.Position = currentPosition;
                    charData.Scale = 1f;
                    charData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 1f * drawer.GetCharacterInfo(charIndex).Advance;
                }
            }
        }
    }
}