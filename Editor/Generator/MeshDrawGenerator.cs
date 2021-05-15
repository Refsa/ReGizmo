
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class MeshDrawGenerator : DrawGenerator
    {
        public MeshDrawGenerator(string name, string drawerName)
        {
            methodName = name;
            fileName = $"{name}Draw.generated.cs";

            methodShell =
@"
        public static void $METHOD_NAME($PARAMS)
        {
            if (ReGizmoResolver<$DRAWER_NAME>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = $PARAM_1;
                shaderData.Rotation = $PARAM_2.ToVector4();
                shaderData.Scale = $PARAM_3;
                shaderData.Color = $PARAM_4;
            }
        }";
            methodShell = methodShell.Replace("$METHOD_NAME", name).Replace("$DRAWER_NAME", drawerName);

            variables = new Variable[] {
                new Variable(typeof(Vector3), "position", "currentPosition", "currentPosition + position", 255),
                new Variable(typeof(Quaternion), "rotation", "currentRotation", "(currentRotation * rotation)"),
                new Variable(typeof(Vector3), "scale", "Vector3.one", "currentScale + scale"),
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

                content += method;
            }

            return content;
        }
    }
}