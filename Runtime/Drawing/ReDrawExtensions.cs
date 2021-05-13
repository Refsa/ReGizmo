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
                drawer.GetShaderDataBuffer().Copy(polyLine.Points);
            }

            if (polyLine.AutoDispose)
            {
                polyLine.Dispose();
            }
        }

        /// <summary>
        /// Visualizes a raycast
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        public static void Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition;
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

        /// <summary>
        /// Visualizes a SphereCast
        /// 
        /// A blue sphere is shown at the intersection point
        /// </summary>
        /// <param name="origin">center of sphere in world space</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="radius">radius of sphere</param>
        /// <param name="distance">max distance to check</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void SphereCast(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition;
            if (Physics.SphereCast(origin, radius, direction, out var hit, distance, layerMask))
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f);
                ReDraw.Sphere(origin + direction * hit.distance, Vector3.one * radius,
                    Color.green.WithAlpha(0.5f));
                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }
        
        /// <summary>
        /// Visualize an overlap sphere
        /// </summary>
        /// <param name="origin">world position of sphere</param>
        /// <param name="radius">radius of sphere</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapSphere(Vector3 origin, float radius, int layerMask = ~0)
        {
            var hits = Physics.OverlapSphere(origin, radius, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.Sphere(origin, Quaternion.identity, Vector3.one * radius, Color.green.WithAlpha(0.5f));
                ReDraw.TextSDF($"{hits.Length}", origin, 12f, Color.white);
            }
            else
            {
                ReDraw.Sphere(origin, Quaternion.identity, Vector3.one * radius, Color.red.WithAlpha(0.5f));
            }
        }

        /// <summary>
        /// Visualizes a BoxCast
        /// 
        /// A blue sphere is shown at the intersection point
        /// </summary>
        /// <param name="center">Center of box in world space</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="halfExtents">extents of box</param>
        /// <param name="orientation">orientation of box</param>
        /// <param name="distance">max distance to check</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void BoxCast(Vector3 center, Vector3 direction, Vector3 halfExtents, Quaternion orientation, float distance = float.MaxValue, int layerMask = ~0)
        {
            center += currentPosition;
            if (Physics.BoxCast(center, halfExtents, direction, out var hit, orientation, distance, layerMask))
            {
                ReDraw.Ray(center, direction * hit.distance, Color.green, 1f);
                ReDraw.Cube(center + direction * hit.distance, orientation, halfExtents * 2,
                    Color.green.WithAlpha(0.5f));
                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue);
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 1f);
            }
        }

        /// <summary>
        /// Visualize an OverlapBox
        /// </summary>
        /// <param name="center">world position of box</param>
        /// <param name="halfExtents">half extents of box</param>
        /// <param name="orientation">orientation of box</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask = ~0)
        {
            var hits = Physics.OverlapBox(center, halfExtents, orientation, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.Cube(center, Quaternion.identity, halfExtents, Color.green.WithAlpha(0.5f));
                ReDraw.TextSDF($"{hits.Length}", center, 12f, Color.white);
            }
            else
            {
                ReDraw.Cube(center, Quaternion.identity, halfExtents, Color.red.WithAlpha(0.5f));
            }
        }

        /// <summary>
        /// Visualizes a CapsuleCast
        /// 
        /// A blue sphere is shown at the intersection point
        /// </summary>
        /// <param name="point1">center of bottom section</param>
        /// <param name="point2">center of top section</param>
        /// <param name="radius">radius of capsule</param>
        /// <param name="direction">direction to cast capsule in</param>
        /// <param name="distance">max distance to cast capsule</param>
        /// <param name="layerMask">LayerMask to test against</param>
        public static void CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            point1 += currentPosition;
            point2 += currentPosition;
            Vector3 center = (point1 + point2) * 0.5f;
            if (Physics.CapsuleCast(point1, point2, radius, direction, out var hit, distance, layerMask))
            {
                Vector3 orientation = (point2 - point1).normalized;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, orientation);

                ReDraw.Ray(center, direction * hit.distance, Color.green, 1f);

                ReDraw.Capsule(center + direction * hit.distance, rotation, Vector3.one * radius * 2, Color.green.WithAlpha(0.5f));

                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue);
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 1f);
            }
        }

        /// <summary>
        /// Visualize an OverlapCapsule
        /// </summary>
        /// <param name="point1">bottom center of capsule</param>
        /// <param name="point2">top center of capsule</param>
        /// <param name="radius">radius of capsule</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapCapsule(Vector3 point1, Vector3 point2, float radius, int layerMask = ~0)
        {
            point1 += currentPosition;
            point2 += currentPosition;
            Vector3 center = (point1 + point2) * 0.5f;

            Vector3 orientation = (point2 - point1).normalized;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, orientation);

            var hits = Physics.OverlapCapsule(point1, point2, radius, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.Capsule(center, rotation, Vector3.one * radius * 2f, Color.green.WithAlpha(0.5f));
                ReDraw.TextSDF($"{hits.Length}", center, 12f, Color.white);
            }
            else
            {
                ReDraw.Capsule(center, rotation, Vector3.one * radius * 2f, Color.red.WithAlpha(0.5f));
            }
        }

        /// <summary>
        /// Visualize a 2D Raycast
        /// </summary>
        /// <param name="origin">Start point of ray</param>
        /// <param name="direction">direction of ray</param>
        /// <param name="distance">distance to check</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void Raycast2D(Vector2 origin, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.Raycast(origin, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Line(origin, hit.point, Color.green, 1f);
                ReDraw.Ray(hit.point, hit.normal * 0.2f, Color.blue, 1f);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }

        /// <summary>
        /// Visualize a 2D BoxCast
        /// </summary>
        /// <param name="origin">center point of box</param>
        /// <param name="size">size of box</param>
        /// <param name="angle">rotation of box on Z-axis</param>
        /// <param name="direction">direction to cast box in</param>
        /// <param name="distance">max distance to check</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void BoxCast2D(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f);
                ReDraw.Quad(origin + direction * hit.distance, Quaternion.Euler(0f, 180f, angle), size, Color.green.WithAlpha(0.5f));
                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }

        /// <summary>
        /// visualize a 2D circlecast
        /// </summary>
        /// <param name="origin">center of circle in world space</param>
        /// <param name="radius">radius of circle</param>
        /// <param name="direction">direction of cast</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">LayerMask to test against</param>
        public static void CircleCast2D(Vector2 origin, float radius, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f);
                ReDraw.Circle(origin + direction * hit.distance, Vector3.back, DrawMode.AxisAligned, Size.Units(radius), FillMode.Fill, Color.green.WithAlpha(0.5f));
                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
            }
        }

        /// <summary>
        /// Visualize a 2D CapsuleCast
        /// </summary>
        /// <param name="origin">Center of capsule in world space</param>
        /// <param name="size">Dimensions of capsule</param>
        /// <param name="capsuleDirection">Orientation of capsule</param>
        /// <param name="angle">rotation of capsule</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">LayerMask to test against</param>
        public static void CapsuleCast2D(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f);

                Vector3 normal = capsuleDirection == CapsuleDirection2D.Vertical ? Vector2.up : Vector2.right;
                Vector3 dir = Quaternion.Euler(0f, 0f, angle) * normal;
                Quaternion rotation = Quaternion.FromToRotation(normal, dir);

                ReDraw.Capsule(origin + direction * hit.distance, rotation, size, Color.green.WithAlpha(0.5f));

                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f);
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