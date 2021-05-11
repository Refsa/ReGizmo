
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | radius.SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode;
            }
        }
        public static void Circle(System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Circle()
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Thickness = data.Radius;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
    }
}