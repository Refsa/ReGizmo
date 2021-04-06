
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Ray(Vector3 origin, Vector3 direction, UnityEngine.Color color, System.Single width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + origin;
                shaderData.Color = color;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = (currentPosition + origin) + direction;
                shaderData.Color = color;
                shaderData.Width = width;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + origin;
                shaderData.Color = color;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = (currentPosition + origin) + direction;
                shaderData.Color = color;
                shaderData.Width = 1f;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction, System.Single width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + origin;
                shaderData.Color = currentColor;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = (currentPosition + origin) + direction;
                shaderData.Color = currentColor;
                shaderData.Width = width;
            }
        }
        public static void Ray(Vector3 origin, Vector3 direction)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + origin;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = (currentPosition + origin) + direction;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
            }
        }
    }
}