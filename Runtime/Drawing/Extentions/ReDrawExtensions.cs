using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = currentPosition + p1;
                shaderData.Position2 = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = currentPosition + p1;
                shaderData.Position2 = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, float width)
        {
            Vector3 vecColor = currentColor.ToVector3();
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = currentPosition + p1;
                shaderData.Position2 = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            Vector3 vecColor = currentColor.ToVector3();
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = currentPosition + p1;
                shaderData.Position2 = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;
            }
        }

        public static void PolyLine(in PolyLine polyLine)
        {
            polyLine.Build();

            if (ReGizmoResolver<ReGizmoPolyLineDrawer>.TryGet(out var drawer))
            {
                drawer.GetShaderDataBuffer().Copy(polyLine.Points);
            }

            if (polyLine.AutoDispose)
            {
                polyLine.Dispose();
            }
        }

        /// <summary>
        /// Draws a sprite at the given position
        /// 
        /// Uses the sprites PixelsPerUnit mutliplied by the given scale as size in pixels
        /// </summary>
        /// <param name="sprite">Sprite to draw</param>
        /// <param name="pos">World position</param>
        /// <param name="scale">Scale, internally multiplied with PPU</param>
        /// <param name="sorting">No functionality atm</param>
        public static void Sprite(Sprite sprite, Vector3 pos, float scale, float sorting = 0f)
        {
            if (ReGizmoResolver<ReGizmoSpritesDrawer>.TryGet(out var drawers))
            {
                ref var data = ref drawers.GetShaderData(sprite);

                data.Position = currentPosition + pos + Vector3.back * sorting;
                data.Scale = sprite.pixelsPerUnit * scale;
            }
        }

        public static void Rect(Rect rect, Color color, float depth = 0f)
        {
            Vector3 p1 = currentPosition + new Vector3(rect.xMin, rect.yMin, depth);
            Vector3 p2 = currentPosition + new Vector3(rect.xMin, rect.yMax, depth);
            Vector3 p3 = currentPosition + new Vector3(rect.xMax, rect.yMax, depth);
            Vector3 p4 = currentPosition + new Vector3(rect.xMax, rect.yMin, depth);

            Vector3 vecColor = color.ToVector3();

            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p1;
                shaderData.Position2 = p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p2;
                shaderData.Position2 = p3;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p3;
                shaderData.Position2 = p4;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p4;
                shaderData.Position2 = p1;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;
            }
        }

        public static void Grid(Vector3 origin, Color lineColor, int distance = 1000, bool inPlace = true, GridPlane plane = GridPlane.XZ)
        {
            if (ReGizmoResolver<GridDrawer>.TryGet(out var drawer))
            {
                var color = lineColor.ToVector3();

                ref var shaderData = ref drawer.GetShaderData();

                shaderData.LineColor = color;
                shaderData.Position = origin;
                shaderData.Range = distance;
                shaderData.Flags = 0;

                if (inPlace)
                {
                    shaderData.Flags |= (uint)GridMode.Infinite;
                }
                else
                {
                    shaderData.Flags |= (uint)GridMode.Static;
                }

                shaderData.Flags |= (uint)plane;
            }
        }

        public static void WireSphere(Vector3 center, Quaternion orientation, float radius, Color color)
        {
            ReDraw.Circle(center, orientation * Vector3.up, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color);
            ReDraw.Circle(center, orientation * Vector3.right, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color);
            ReDraw.Circle(center, orientation * Vector3.forward, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color);
        }

        public static void WireCapsule(Vector3 center, Quaternion orientation, float radius, float height, Color color)
        {
            Vector3 dir = orientation * Vector3.up;
            float halfHeight = height * 0.5f;

            Vector3 top = center + dir * halfHeight;
            Vector3 bottom = center - dir * halfHeight;

            WireSphere(top, orientation, radius, color);
            WireSphere(bottom, orientation, radius, color);

            Vector3 perp1 = Mathf.Approximately(Mathf.Abs(Vector3.Dot(dir, Vector3.right)), 1f) ?
                Vector3.Cross(dir, Vector3.up).normalized * radius
                :
                Vector3.Cross(dir, Vector3.right).normalized * radius;

            Vector3 perp2 = Vector3.Cross(dir, perp1).normalized * radius;

            Line(bottom + perp1, top + perp1, color, 1f);
            Line(bottom - perp1, top - perp1, color, 1f);

            Line(bottom + perp2, top + perp2, color, 1f);
            Line(bottom - perp2, top - perp2, color, 1f);
        }

        public static void WireCube(Vector3 center, Quaternion rotation, Vector3 extents, Color color)
        {
            var halfExtents = extents / 2f;

            Vector3 p0 = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);
            Vector3 p1 = center + rotation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
            Vector3 p2 = center + rotation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            Vector3 p3 = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            Vector3 p4 = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
            Vector3 p5 = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
            Vector3 p6 = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
            Vector3 p7 = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);

            // TOP
            Line(p0, p1, color, 1f);
            Line(p1, p2, color, 1f);
            Line(p2, p3, color, 1f);
            Line(p3, p0, color, 1f);

            // BOTTOM
            Line(p4, p5, color, 1f);
            Line(p5, p6, color, 1f);
            Line(p6, p7, color, 1f);
            Line(p7, p4, color, 1f);

            // CONNECTORS
            Line(p0, p4, color, 1f);
            Line(p1, p5, color, 1f);
            Line(p2, p6, color, 1f);
            Line(p3, p7, color, 1f);
        }

        public static void Circle2(Vector3 center, Vector3 normal, float radius, int resolution)
        {
            float theta = 0f;
            float step = 360f / (float)resolution;
            Vector3 dir = Vector3.right;
            if (Mathf.Abs(Vector3.Dot(dir, normal)) == 1.0) dir = Vector3.up;
            dir = Vector3.Cross(dir, normal);

            Vector3 prevPoint = center + Quaternion.Euler(normal * theta) * dir * radius;
            for (int i = 1; i < resolution + 2; i++)
            {
                Vector3 point = center + Quaternion.Euler(normal * theta) * dir * radius;

                Line(prevPoint, point, Color.red, 1f);
                prevPoint = point;
                theta += step;
            }
        }

#if REGIZMO_DEV
        static void AABBDebugLine(Vector3 p1, Vector3 p2, Color color, float width)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<AABBDebugDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = currentPosition + p1;
                shaderData.Position2 = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = width;
            }
        }

        public static void AABBDebug(Vector3 center, Quaternion rotation, Vector3 extents, Color color)
        {
            var halfExtents = extents / 2f;

            Vector3 p0 = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);
            Vector3 p1 = center + rotation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
            Vector3 p2 = center + rotation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            Vector3 p3 = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            Vector3 p4 = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
            Vector3 p5 = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
            Vector3 p6 = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
            Vector3 p7 = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);

            // TOP
            AABBDebugLine(p0, p1, color, 1f);
            AABBDebugLine(p1, p2, color, 1f);
            AABBDebugLine(p2, p3, color, 1f);
            AABBDebugLine(p3, p0, color, 1f);

            // BOTTOM
            AABBDebugLine(p4, p5, color, 1f);
            AABBDebugLine(p5, p6, color, 1f);
            AABBDebugLine(p6, p7, color, 1f);
            AABBDebugLine(p7, p4, color, 1f);

            // CONNECTORS
            AABBDebugLine(p0, p4, color, 1f);
            AABBDebugLine(p1, p5, color, 1f);
            AABBDebugLine(p2, p6, color, 1f);
            AABBDebugLine(p3, p7, color, 1f);
        }
#endif
    }
}