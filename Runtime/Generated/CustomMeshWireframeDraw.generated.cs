
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles + rotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void WireframeMesh(Mesh mesh, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void WireframeMesh(Mesh mesh)
        {
            if (ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.eulerAngles * Mathf.Deg2Rad;
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
    }
}