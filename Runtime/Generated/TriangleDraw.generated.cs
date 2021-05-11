
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.Size width, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = width.Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | width.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Triangle(ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Width = Size.Pixels(32f).Value;
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = new Vector3(color.r, color.g, color.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
    }
}