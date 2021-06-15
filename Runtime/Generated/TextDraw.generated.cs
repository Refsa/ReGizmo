
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Text(string text, UnityEngine.Vector3 position, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = position.Add(currentPosition);
                textData.Scale = scale;
                textData.Color.Copy(color);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

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
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = position.Add(currentPosition);
                textData.Scale = scale;
                textData.Color.Copy(currentColor);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

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
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = position.Add(currentPosition);
                textData.Scale = 16f;
                textData.Color.Copy(color);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 16f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, System.Single scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = scale;
                textData.Color.Copy(color);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

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
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = position.Add(currentPosition);
                textData.Scale = 16f;
                textData.Color.Copy(currentColor);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 16f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text, System.Single scale)
        {
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = scale;
                textData.Color.Copy(currentColor);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

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
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = 16f;
                textData.Color.Copy(color);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 16f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
        public static void Text(string text)
        {
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = currentPosition;
                textData.Scale = 16f;
                textData.Color.Copy(currentColor);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += 16f * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }
    }
}