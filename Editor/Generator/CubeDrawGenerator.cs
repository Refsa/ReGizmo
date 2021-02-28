
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class CubeDrawGenerator : DrawGenerator
    {
        public CubeDrawGenerator()
        {
            methodName = "Cube";
            fileName = "CubeDraw.generated.cs";

            methodShell =
@"public static void Cube($PARAMS)
{
    if (ReGizmoResolver<ReGizmoCubeDrawer>.TryGet(out var drawer))
    {
        ref var shaderData = ref drawer.GetShaderData();
    
        shaderData.Position = $PARAM_1;
        shaderData.Rotation = $PARAM_2.eulerAngles * Mathf.Deg2Rad;
        shaderData.Scale = $PARAM_3;
        shaderData.Color = $PARAM_4;
    }
}";

            variables = new Variable[] {
                new Variable(typeof(Vector3), "position", "currentPosition", "position"),
                new Variable(typeof(Quaternion), "rotation", "currentRotation", "rotation"),
                new Variable(typeof(Vector3), "scale", "currentScale", "scale"),
                new Variable(typeof(Color), "color", "currentColor", "color") };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                method = method.Replace("$PARAMS", perm.Item2);

                string[] chars = perm.Item1.Split(',');
                method = method
                    .Replace("$PARAM_1", chars[0].Trim())
                    .Replace("$PARAM_2", chars[1].Trim())
                    .Replace("$PARAM_3", chars[2].Trim())
                    .Replace("$PARAM_4", chars[3].Trim());

                content += method + "\n";
            }

            return content;
        }
    }
}