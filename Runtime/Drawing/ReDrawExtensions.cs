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
                shaderData.Color = color1;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
            }
        }

        public static void PolyLine(in PolyLine polyLine)
        {
            polyLine.Build();

            if (ReGizmoResolver<ReGizmoPolyLineDrawer>.TryGet(out var drawer))
            {
                foreach (var point in polyLine.Points)
                {
                    ref var shaderData = ref drawer.GetShaderData();

                    shaderData.Position = point.Position + currentPosition;
                    shaderData.Color = point.Color;
                    shaderData.ID = point.ID;
                    shaderData.Width = point.Width;
                }
            }

            if (polyLine.AutoDispose)
            {
                polyLine.Dispose();
            }
        }

        public static void Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue,
            int layerMask = ~0)
        {
            if (Physics.Raycast(origin, direction, out var hit, distance, layerMask))
            {
                ReDraw.Line(origin, hit.point, Color.green, 1f);
                ReDraw.Ray(hit.point, hit.normal * 0.2f, Color.blue, 1f);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }

        public static void SphereCast(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue,
            int layerMask = ~0)
        {
            if (Physics.SphereCast(origin, radius, direction, out var hit, distance, layerMask))
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f);
                ReDraw.Sphere(origin + direction * hit.distance, Vector3.one * radius,
                    Color.green.WithAlpha(0.5f));
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }

        public static void BoxCast(Vector3 center, Vector3 direction, Vector3 halfExtents, Quaternion orientation,
            float distance = float.MaxValue, int layerMask = ~0)
        {
            if (Physics.BoxCast(center, halfExtents, direction, out var hit, orientation, distance, layerMask))
            {
                ReDraw.Ray(center, direction * hit.distance, Color.green, 1f);
                ReDraw.Cube(center + direction * hit.distance, orientation, halfExtents * 2,
                    Color.green.WithAlpha(0.5f));
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 1f);
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
            Vector3 p1 = new Vector3(rect.xMin, rect.yMin, depth);
            Vector3 p2 = new Vector3(rect.xMin, rect.yMax, depth);
            Vector3 p3 = new Vector3(rect.xMax, rect.yMax, depth);
            Vector3 p4 = new Vector3(rect.xMax, rect.yMin, depth);

            Line(p1, p2, color, 1f);
            Line(p2, p3, color, 1f);
            Line(p3, p4, color, 1f);
            Line(p4, p1, color, 1f);
        }
    }
}