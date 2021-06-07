
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, UnityEngine.Color color, ReGizmo.Drawing.Size size)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + size.Value;
                shaderData.Flags = size.SizeMode;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + Size.Pixels(32f).Value;
                shaderData.Flags = Size.Pixels(32f).SizeMode;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position, ReGizmo.Drawing.Size size)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + size.Value;
                shaderData.Flags = size.SizeMode;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Color color, ReGizmo.Drawing.Size size)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + size.Value;
                shaderData.Flags = size.SizeMode;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + position;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + Size.Pixels(32f).Value;
                shaderData.Flags = Size.Pixels(32f).SizeMode;
            }
        }
        public static void Icon(Texture2D texture, UnityEngine.Color color)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(color.r, color.g, color.b);
                shaderData.Scale = currentScale.x + Size.Pixels(32f).Value;
                shaderData.Flags = Size.Pixels(32f).SizeMode;
            }
        }
        public static void Icon(Texture2D texture, ReGizmo.Drawing.Size size)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + size.Value;
                shaderData.Flags = size.SizeMode;
            }
        }
        public static void Icon(Texture2D texture)
        {
            if (ReGizmoResolver<IconsDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(texture);

                shaderData.Position = currentPosition + currentPosition;
                shaderData.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                shaderData.Scale = currentScale.x + Size.Pixels(32f).Value;
                shaderData.Flags = Size.Pixels(32f).SizeMode;
            }
        }
    }
}