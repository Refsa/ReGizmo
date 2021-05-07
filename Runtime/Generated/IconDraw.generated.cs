
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, UnityEngine.Color color, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + scale;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + 32f;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + scale;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Color color, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + scale;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + 32f;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + 32f;
            }
        }
        public static void Icon(Texture2D texture, System.Single scale)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + scale;
            }
        }
        public static void Icon(Texture2D texture)
        {
            if (ReGizmoResolver<ReGizmoIconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + 32f;
            }
        }
    }
}