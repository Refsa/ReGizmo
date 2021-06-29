
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.FillMode fillMode, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<TriangleDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
    }
}