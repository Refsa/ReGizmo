using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color1.ToVector3();
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color2.ToVector3();
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = vecColor;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = vecColor;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            Vector3 vecColor = color.ToVector3();
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            Vector3 vecColor = currentColor.ToVector3();
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
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

            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p2;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p3;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p3;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p4;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p4;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = p1;
                shaderData.Color = vecColor;
                shaderData.Width = 1f;
            }
        }

        public static void Grid(Vector3 origin, Vector2 cellSize, Vector3 normal, float lineWidth, Color lineColor, int distance = 1000)
        {
            if (!ReGizmoResolver<ReGizmoGridDrawer>.TryGet(out var drawer))
            {
                return;
            }

            var color = lineColor.ToVector3();
            var offColor = color * 0.33f;

            for (int x = -distance; x < distance; x++)
            {
                ref var data = ref drawer.GetShaderData();
                var c = x % 10 == 0 ? color : offColor;

                data.Position1 = origin + new Vector3(x, 0f, -distance);
                data.Position2 = origin + new Vector3(x, 0f, distance);

                data.Color = c;
                data.Width = lineWidth;
            }

            for (int y = -distance; y < distance; y++)
            {
                ref var data = ref drawer.GetShaderData();
                var c = y % 10 == 0 ? color : offColor;

                data.Position1 = origin + new Vector3(-distance, 0f, y);
                data.Position2 = origin + new Vector3(distance, 0f, y);

                data.Color = c;
                data.Width = lineWidth;
            }
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
    }
}