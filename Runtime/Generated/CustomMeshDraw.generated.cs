
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
        public static void Mesh(Mesh mesh, UnityEngine.Color color)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = color;
            }
        }
        public static void Mesh(Mesh mesh)
        {
            if (ReGizmoResolver<CustomMeshDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData(mesh);

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale;
                shaderData.Color = currentColor;
            }
        }
    }
}