
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(System.Single thickness, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode;
            }
        }
        public static void Triangle(System.Single thickness)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = thickness;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle(UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
        public static void Triangle()
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Thickness = 1f;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode;
            }
        }
    }
}