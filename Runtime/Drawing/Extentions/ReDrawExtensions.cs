using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color, float width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1.SetAdd(currentPosition, p1);
                shaderData.Position2.SetAdd(currentPosition, p2);
                shaderData.Color.Copy(color);
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1.SetAdd(p1, currentPosition);
                shaderData.Position2.SetAdd(p2, currentPosition);
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, float width, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1.SetAdd(currentPosition, p1);
                shaderData.Position2.SetAdd(currentPosition, p2);
                shaderData.Color.Copy(currentColor);
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1.SetAdd(currentPosition, p1);
                shaderData.Position2.SetAdd(currentPosition, p2);
                shaderData.Color.Copy(currentColor);
                shaderData.Width = 1f;
            }
        }

        public static void PolyLine(in PolyLine polyLine)
        {
            polyLine.Build();

            if (ReGizmoResolver<PolyLineDrawer>.TryGet(out var drawer, polyLine.depthMode))
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
        public static void Sprite(Sprite sprite, Vector3 pos, float scale, float sorting = 0f, DepthMode depthMode = DepthMode.Sorted)
        {
            if (ReGizmoResolver<SpritesDrawer>.TryGet(out var drawers, depthMode))
            {
                ref var data = ref drawers.GetShaderData(sprite);

                data.Position.SetAdd(currentPosition, pos);
                data.Scale = sprite.pixelsPerUnit * scale;
            }
        }

        public static void Texture(Texture texture, Vector3 pos, Quaternion rotation, float scale)
        {
            if (ReGizmoResolver<TexturesDrawer>.TryGet(out var drawers))
            {
                ref var data = ref drawers.GetShaderData(texture);

                float aspect = (float)texture.width / (float)texture.height;

                data.Position = pos.Add(currentPosition);
                data.Rotation.Copy(rotation);
                data.Scale.Set(scale, scale, 1f);
                data.Color = Color.white;
            }
        }

        public static void Rect(Rect rect, Color color, float depth = 0f, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 p1 = new Vector3(rect.xMin + currentPosition.x, rect.yMin + currentPosition.y, depth + currentPosition.z);
            Vector3 p2 = new Vector3(rect.xMin + currentPosition.x, rect.yMax + currentPosition.y, depth + currentPosition.z);
            Vector3 p3 = new Vector3(rect.xMax + currentPosition.x, rect.yMax + currentPosition.y, depth + currentPosition.z);
            Vector3 p4 = new Vector3(rect.xMax + currentPosition.x, rect.yMin + currentPosition.y, depth + currentPosition.z);

            if (ReGizmoResolver<LineDrawer>.TryGet(out var drawer, depthMode))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p1;
                shaderData.Position2 = p2;
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p2;
                shaderData.Position2 = p3;
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p3;
                shaderData.Position2 = p4;
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position1 = p4;
                shaderData.Position2 = p1;
                shaderData.Color.Copy(color);
                shaderData.Width = 1f;
            }
        }

        public static void Grid(Vector3 origin, Color lineColor, int distance = 1000, bool inPlace = true, GridPlane plane = GridPlane.XZ)
        {
            if (ReGizmoResolver<GridDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.LineColor.Copy(lineColor);
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

        public static void WireSphere(Vector3 center, Quaternion orientation, float radius, Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            ReDraw.Circle(center, orientation * V3Up, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color, depthMode);
            ReDraw.Circle(center, orientation * V3Right, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color, depthMode);
            ReDraw.Circle(center, orientation * V3Forward, DrawMode.AxisAligned, Size.Units(radius), FillMode.Outline, color, depthMode);
        }

        public static void WireCapsule(Vector3 center, Quaternion orientation, float radius, float height, Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 dir = orientation * V3Up;
            float halfHeight = height * 0.5f;

            Vector3 top = center + dir * halfHeight;
            Vector3 bottom = center - dir * halfHeight;

            WireSphere(top, orientation, radius, color, depthMode);
            WireSphere(bottom, orientation, radius, color, depthMode);

            Vector3 perp1 = Mathf.Approximately(Mathf.Abs(Vector3.Dot(dir, V3Right)), 1f) ?
                Vector3.Cross(dir, V3Up).normalized
                :
                Vector3.Cross(dir, V3Right).normalized;

            Vector3 perp2 = Vector3.Cross(dir, perp1);

            perp1.Mul(radius);
            perp2.Mul(radius);

            Line(bottom + perp1, top + perp1, color, 1f, depthMode);
            Line(bottom - perp1, top - perp1, color, 1f, depthMode);

            Line(bottom + perp2, top + perp2, color, 1f, depthMode);
            Line(bottom - perp2, top - perp2, color, 1f, depthMode);
        }

        public static void WireCube(Vector3 center, Quaternion rotation, Vector3 extents, Color color, DepthMode depthMode = DepthMode.Sorted)
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
            Line(p0, p1, color, 1f, depthMode);
            Line(p1, p2, color, 1f, depthMode);
            Line(p2, p3, color, 1f, depthMode);
            Line(p3, p0, color, 1f, depthMode);

            // BOTTOM
            Line(p4, p5, color, 1f, depthMode);
            Line(p5, p6, color, 1f, depthMode);
            Line(p6, p7, color, 1f, depthMode);
            Line(p7, p4, color, 1f, depthMode);

            // CONNECTORS
            Line(p0, p4, color, 1f, depthMode);
            Line(p1, p5, color, 1f, depthMode);
            Line(p2, p6, color, 1f, depthMode);
            Line(p3, p7, color, 1f, depthMode);
        }

        public static void Circle2(Vector3 center, Vector3 normal, float radius, int resolution, DepthMode depthMode = DepthMode.Sorted)
        {
            float theta = 0f;
            float step = 360f / (float)resolution;
            Vector3 dir = V3Right;
            if (Mathf.Abs(Vector3.Dot(dir, normal)) == 1.0) dir = V3Up;
            dir = Vector3.Cross(dir, normal);

            Vector3 prevPoint = center + Quaternion.Euler(normal * theta) * dir * radius;
            for (int i = 1; i < resolution + 2; i++)
            {
                Vector3 point = center + Quaternion.Euler(normal * theta) * dir * radius;

                Line(prevPoint, point, Color.red, 1f, depthMode);
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