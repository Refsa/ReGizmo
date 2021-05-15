
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = position;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(UnityEngine.Vector3 normal, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = normal;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(ReGizmo.Drawing.DrawMode drawMode, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(ReGizmo.Drawing.Size radius, ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = radius.Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(ReGizmo.Drawing.FillMode fillMode, UnityEngine.Color color)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)drawMode | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | radius.SizeMode | (int)FillMode.Fill;
            }
        }
        public static void Circle(ReGizmo.Drawing.FillMode fillMode)
        {
            if (ReGizmoResolver<ReGizmoCircleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = currentPosition;
                data.Normal = Vector3.up;
                data.Radius = Size.Pixels(32f).Value;
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)fillMode;
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
                data.Color = color;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
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
                data.Color = currentColor;
                data.Flags = (int)DrawMode.BillboardFree | Size.Pixels(32f).SizeMode | (int)FillMode.Fill;
            }
        }
    }
}