
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Vector3 scale)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position, UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron(UnityEngine.Quaternion rotation, UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron(UnityEngine.Vector3 position)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Octahedron(UnityEngine.Quaternion rotation)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Octahedron(UnityEngine.Color color)
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Octahedron()
        {
            if (ReGizmoResolver<OctahedronDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
    }
}