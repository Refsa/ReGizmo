
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Vector3 scale, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = scale.Add(currentScale);
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(UnityEngine.Quaternion rotation, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(UnityEngine.Vector3 position, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position.Add(currentPosition);
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Sphere(UnityEngine.Quaternion rotation, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy((currentRotation * rotation));
                shaderData.Scale = V3One;
                shaderData.Color.Copy(currentColor);
            }
        }
        public static void Sphere(UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = currentPosition;
                shaderData.Rotation.Copy(currentRotation);
                shaderData.Scale = V3One;
                shaderData.Color.Copy(color);
            }
        }
        public static void Sphere(DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SphereDrawer>.TryGet(out var drawer, depthMode))
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