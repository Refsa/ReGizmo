using UnityEngine;

namespace ReGizmo.Generator
{
    internal class RayDrawGenerator : DrawGenerator
    {
        public RayDrawGenerator()
        {
            methodName = "Ray";
            fileName = $"RayDraw.generated.cs";

            methodShell =
                @"
        public static void Ray($PARAMS)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + origin;
                shaderData.Color = $PARAM_1;
                shaderData.Width = $PARAM_2;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = (currentPosition + origin) + direction;
                shaderData.Color = $PARAM_1;
                shaderData.Width = $PARAM_2;
            }
        }";
            variables = new Variable[]
            {
                new Variable(typeof(Color), "color", "currentColor", "color", 255),
                new Variable(typeof(float), "width", "1f", "width"),
            };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                string parameters = "Vector3 origin, Vector3 direction";
                if (!string.IsNullOrEmpty(perm.Item2))
                {
                    parameters += ", " + perm.Item2;
                }

                method = method.Replace("$PARAMS", parameters);

                string[] chars = perm.Item1.Split(',');
                method = method
                    .Replace("$PARAM_1", chars[0].Trim())
                    .Replace("$PARAM_2", chars[1].Trim());

                content += method;
            }

            return content;
        }
    }
}