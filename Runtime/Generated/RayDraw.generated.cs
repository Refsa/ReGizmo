
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Ray(Vector3 origin, Vector3 direction, UnityEngine.Color color, System.Single width)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = origin.Add(currentPosition);
                shaderData.Position2 = direction.Add(shaderData.Position1);
                shaderData.Color.Copy(color);
                shaderData.Width = width;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction, UnityEngine.Color color)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = origin.Add(currentPosition);
                shaderData.Position2 = direction.Add(shaderData.Position1);
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction, System.Single width)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = origin.Add(currentPosition);
                shaderData.Position2 = direction.Add(shaderData.Position1);
                shaderData.Color.Copy(currentColor);
                shaderData.Width = width;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = origin.Add(currentPosition);
                shaderData.Position2 = direction.Add(shaderData.Position1);
                shaderData.Color.Copy(currentColor);
                shaderData.Width = 1f;
            }
        }
    }
}