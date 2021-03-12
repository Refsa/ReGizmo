
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color1;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
            }
        }
    }
}