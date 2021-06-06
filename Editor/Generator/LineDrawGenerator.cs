
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class LineDrawGenerator : DrawGenerator
    {
        public LineDrawGenerator()
        {
            methodName = "Line";
            fileName = $"LineDraw.generated.cs";

            methodShell =
@"
        public static void Line($PARAMS)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = point1;
                shaderData.Position2 = point2;
                shaderData.Color = $PARAM_1;
                shaderData.Width = $PARAM_3;
            }
        }";
            variables = new Variable[] {
                new Variable(typeof(Color), "color1", "currentColor", "color1", 255),
                new Variable(typeof(Color), "color2", "currentColor", "color2"),
                new Variable(typeof(float), "width1", "1f", "width1", 255),
                new Variable(typeof(float), "width2", "1f", "width2"),
            };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                string parameters = "Vector3 point1, Vector3 point2";
                if (!string.IsNullOrEmpty(perm.Item2))
                {
                    parameters += ", " + perm.Item2;
                }
                method = method.Replace("$PARAMS", parameters);

                string[] chars = perm.Item1.Split(',');
                method = method
                    .Replace("$PARAM_1", chars[0].Trim())
                    .Replace("$PARAM_2", chars[1].Trim())
                    .Replace("$PARAM_3", chars[2].Trim())
                    .Replace("$PARAM_4", chars[3].Trim());

                content += method;
            }

            return content;
        }
    }
}