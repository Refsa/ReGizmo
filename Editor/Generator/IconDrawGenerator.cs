
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class IconDrawGenerator : DrawGenerator
    {
        public IconDrawGenerator()
        {
            methodName = "Icon";
            fileName = $"IconDraw.generated.cs";

            methodShell =
@"
        public static void Icon($PARAMS)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + $PARAM_1;
                shaderData.Color = new Vector3($PARAM_2.r, $PARAM_2.g, $PARAM_2.b);
                shaderData.Scale = currentScale.x + $PARAM_3.Value;
                shaderData.Flags = $PARAM_3.SizeMode;
            }
        }";
            variables = new Variable[] {
                new Variable(typeof(Vector3), "position", "currentPosition", "position"),
                new Variable(typeof(Color), "color", "currentColor", "color"),
                new Variable(typeof(Size), "size", "Size.Pixels(32f)", "size"),
            };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                string parameters = "Texture2D texture";
                if (!string.IsNullOrEmpty(perm.Item2))
                {
                    parameters += ", " + perm.Item2;
                }
                method = method.Replace("$PARAMS", parameters);

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