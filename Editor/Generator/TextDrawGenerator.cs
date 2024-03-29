using UnityEngine;

namespace ReGizmo.Generator
{
    internal class TextDrawGenerator : DrawGenerator
    {
        public TextDrawGenerator()
        {
            methodName = "Text";
            fileName = $"TextDraw.generated.cs";

            methodShell =
                @"
        public static void Text($PARAMS)
        {
            if (ReGizmoResolver<TextDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var textData = ref drawer.GetTextShaderData(out uint id);
                textData.Position = $PARAM_1;
                textData.Scale = $PARAM_2;
                textData.Color.Copy($PARAM_3);

                var charDatas = drawer.GetShaderDataRange(text.Length);
                
                float totalAdvance = 0f;
                for (int i = 0; i < text.Length; i++)
                {
                    ref var charData = ref charDatas.Next();

                    charData.TextID = id;
                    charData.Advance = totalAdvance;

                    uint charIndex = (uint)text[i];
                    charData.CharIndex = charIndex;

                    totalAdvance += $PARAM_2 * drawer.GetCharacterInfo(charIndex).Advance;
                }

                textData.CenterOffset = totalAdvance / 2.0f;
            }
        }";
            variables = new Variable[]
            {
                new Variable(typeof(Vector3), "position", "currentPosition", "position.Add(currentPosition)", 255),
                new Variable(typeof(float), "scale", "16f", "scale"),
                new Variable(typeof(Color), "color", "currentColor", "color")
            };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                string arguments = "string text, ";
                if (!string.IsNullOrEmpty(perm.Item2))
                {
                    arguments += perm.Item2 + ", ";
                }
                arguments += "DepthMode depthMode = DepthMode.Sorted";

                method = method.Replace("$PARAMS", arguments);

                string[] chars = perm.Item1.Split(',');
                method = method
                    .Replace("$PARAM_1", chars[0].Trim())
                    .Replace("$PARAM_2", chars[1].Trim())
                    .Replace("$PARAM_3", chars[2].Trim());

                content += method;
            }

            return content;
        }
    }
}