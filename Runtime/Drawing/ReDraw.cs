
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        internal static Vector3 currentPosition = Vector3.zero;
        internal static Quaternion currentRotation = Quaternion.identity;
        internal static Vector3 currentScale = Vector3.zero;
        internal static Color currentColor = Color.white;

        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = color1;
                shaderData.Width = width1;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
                shaderData.Mode = 1;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = color;
                shaderData.Width = width1;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = color;
                shaderData.Width = width2;
                shaderData.Mode = 1;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = color;
                shaderData.Width = width;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = color;
                shaderData.Width = width;
                shaderData.Mode = 1;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = color;
                shaderData.Width = 1f;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = color;
                shaderData.Width = 1f;
                shaderData.Mode = 1;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
                shaderData.Mode = 1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
                shaderData.Mode = 1;
            }
        }
    }
}