
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Cone(UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Cone(UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Cone(UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Cone(UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation.eulerAngles + rotation.eulerAngles) * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Cone(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Cone()
        {
            if (ReGizmoResolver<ReGizmoConeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
    }
}