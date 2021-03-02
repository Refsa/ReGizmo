
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class CustomMeshDrawGenerator : DrawGenerator
    {
        public CustomMeshDrawGenerator()
        {
            methodName = "Mesh";
            fileName = $"CustomMeshDraw.generated.cs";

            methodShell =
@"
        public static void Mesh($PARAMS)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = $PARAM_1;
                shaderData.Rotation = $PARAM_2.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = $PARAM_3;
                shaderData.Color = $PARAM_4;
            }
        }";
            variables = new Variable[] {
                new Variable(typeof(Vector3), "position", "currentPosition", "currentPosition + position", 255),
                new Variable(typeof(Quaternion), "rotation", "currentRotation", "currentRotation.eulerAngles + rotation"),
                new Variable(typeof(Vector3), "scale", "currentScale", "currentScale + scale"),
                new Variable(typeof(Color), "color", "currentColor", "color") };
        }

        protected override string GenerateInternal()
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;
                string parameters = "Mesh mesh";
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