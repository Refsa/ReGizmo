
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = color;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = currentScale + scale;
                shaderData.Color = currentColor;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Octahedron(UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition + position;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Octahedron(UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = (currentRotation * rotation).ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
        public static void Octahedron(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = color;
            }
        }
        public static void Octahedron()
        {
            if (ReGizmoResolver<ReGizmoOctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation = currentRotation.ToVector4();
                shaderData.Scale = Vector3.one;
                shaderData.Color = currentColor;
            }
        }
    }
}