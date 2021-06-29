
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void TextSDF(string text, UnityEngine.Vector3 position, System.Single scale, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, UnityEngine.Vector3 position, System.Single scale, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, UnityEngine.Vector3 position, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, System.Single scale, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, UnityEngine.Vector3 position, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, System.Single scale, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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
        public static void TextSDF(string text, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SDFTextDrawer>.TryGet(out var drawer, depthMode))
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