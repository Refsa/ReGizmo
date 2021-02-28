using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position + currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = scale + currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad(UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (rotation.eulerAngles + currentRotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Quad(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Quad()
        {
            if (ReGizmoResolver<ReGizmoQuadDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }

    }
}