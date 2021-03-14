
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Capsule(UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Capsule(UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Capsule(UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Capsule(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Capsule()
        {
            if (ReGizmoResolver<ReGizmoCapsuleDrawer>.TryGet(out var drawer))
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