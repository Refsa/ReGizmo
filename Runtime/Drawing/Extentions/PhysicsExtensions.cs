using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        /// <summary>
        /// Visualizes a raycast
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        public static void Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition;
            if (Physics.Raycast(origin, direction, out var hit, distance, layerMask))
            {
                ReDraw.Line(origin, hit.point, Color.green, 1f, depthMode);
                ReDraw.Ray(hit.point, hit.normal * 0.2f, Color.blue, 1f, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
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
        public static void SphereCast(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition;
            if (Physics.SphereCast(origin, radius, direction, out var hit, distance, layerMask))
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f, depthMode);
                ReDraw.WireSphere(origin + direction * hit.distance, Quaternion.identity, radius, Color.green, depthMode);
                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an overlap sphere
        /// </summary>
        /// <param name="origin">world position of sphere</param>
        /// <param name="radius">radius of sphere</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapSphere(Vector3 origin, float radius, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            var hits = Physics.OverlapSphere(origin, radius, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.WireSphere(origin, Quaternion.identity, radius, Color.green, depthMode);
                ReDraw.TextSDF($"{hits.Length}", origin, 12f, Color.white, depthMode);
            }
            else
            {
                ReDraw.WireSphere(origin, Quaternion.identity, radius, Color.red, depthMode);
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
        public static void BoxCast(Vector3 center, Vector3 direction, Vector3 halfExtents, Quaternion orientation, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            center += currentPosition;
            if (Physics.BoxCast(center, halfExtents, direction, out var hit, orientation, distance, layerMask))
            {
                ReDraw.Ray(center, direction * hit.distance, Color.green, 1f, depthMode);
                ReDraw.WireCube(center + direction * hit.distance, orientation, halfExtents * 2, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an OverlapBox
        /// </summary>
        /// <param name="center">world position of box</param>
        /// <param name="halfExtents">half extents of box</param>
        /// <param name="orientation">orientation of box</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            var hits = Physics.OverlapBox(center, halfExtents, orientation, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.WireCube(center, Quaternion.identity, halfExtents, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.TextSDF($"{hits.Length}", center, 12f, Color.white, depthMode);
            }
            else
            {
                ReDraw.WireCube(center, Quaternion.identity, halfExtents, Color.red.WithAlpha(0.5f), depthMode);
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
        public static void CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            point1 += currentPosition;
            point2 += currentPosition;
            Vector3 center = (point1 + point2) * 0.5f;
            float height = (point1 - point2).magnitude;

            if (Physics.CapsuleCast(point1, point2, radius, direction, out var hit, distance, layerMask))
            {
                Vector3 orientation = (point2 - point1).normalized;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, orientation);

                ReDraw.Ray(center, direction * hit.distance, Color.green, 1f, depthMode);

                ReDraw.WireCapsule(center + direction * hit.distance, rotation, radius, height, Color.green.WithAlpha(0.5f), depthMode);

                ReDraw.Sphere(hit.point, Vector3.one * 0.05f, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an OverlapCapsule
        /// </summary>
        /// <param name="point1">bottom center of capsule</param>
        /// <param name="point2">top center of capsule</param>
        /// <param name="radius">radius of capsule</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapCapsule(Vector3 point1, Vector3 point2, float radius, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            point1 += currentPosition;
            point2 += currentPosition;
            Vector3 center = (point1 + point2) * 0.5f;
            float height = (point1 - point2).magnitude;

            Vector3 orientation = (point2 - point1).normalized;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, orientation);

            var hits = Physics.OverlapCapsule(point1, point2, radius, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.WireCapsule(center, rotation, radius, height, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.TextSDF($"{hits.Length}", center, 12f, Color.white, depthMode);
            }
            else
            {
                ReDraw.WireCapsule(center, rotation, radius, height, Color.red.WithAlpha(0.5f), depthMode);
            }
        }

        /// <summary>
        /// Visualize a 2D Raycast
        /// </summary>
        /// <param name="origin">Start point of ray</param>
        /// <param name="direction">direction of ray</param>
        /// <param name="distance">distance to check</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void Raycast2D(Vector2 origin, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.Raycast(origin, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Line(origin, hit.point, Color.green, 1f, depthMode);
                ReDraw.Ray(hit.point, hit.normal * 0.2f, Color.blue, 1f, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
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
        public static void BoxCast2D(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f, depthMode);
                ReDraw.Quad(origin + direction * hit.distance, Quaternion.Euler(0f, 180f, angle), size, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an OverlapBoxAll2D
        /// </summary>
        /// <param name="origin">world position of box</param>
        /// <param name="size">size of box</param>
        /// <param name="angle">rotation of box on Z-Axis</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapBox2D(Vector2 origin, Vector2 size, float angle, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            var hits = Physics2D.OverlapBoxAll(origin, size, angle, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.Quad(origin, Quaternion.Euler(0f, 180f, angle), size, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.TextSDF($"{hits.Length}", origin, 12f, Color.white, depthMode);
            }
            else
            {
                ReDraw.Quad(origin, Quaternion.Euler(0f, 180f, angle), size, Color.red.WithAlpha(0.5f), depthMode);
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
        public static void CircleCast2D(Vector2 origin, float radius, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f, depthMode);
                ReDraw.Circle(origin + direction * hit.distance, Vector3.back, DrawMode.AxisAligned, Size.Units(radius), FillMode.Fill, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an OverlapCircleAll2D
        /// </summary>
        /// <param name="origin">world position of circle</param>
        /// <param name="radius">size of circle</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapCircle2D(Vector2 origin, float radius, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            var hits = Physics2D.OverlapCircleAll(origin, radius, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.TextSDF($"{hits.Length}", origin, 12f, Color.white, depthMode);
                ReDraw.Circle(origin, Vector3.back, DrawMode.AxisAligned, Size.Units(radius), FillMode.Fill, Color.green.WithAlpha(0.5f), depthMode);
            }
            else
            {
                ReDraw.Circle(origin, Vector3.back, DrawMode.AxisAligned, Size.Units(radius), FillMode.Fill, Color.red.WithAlpha(0.5f), depthMode);
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
        public static void CapsuleCast2D(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            origin += currentPosition.ToVector2();
            var hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask);
            if (hit.collider != null)
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 1f, depthMode);

                Vector3 normal = capsuleDirection == CapsuleDirection2D.Vertical ? Vector2.up : Vector2.right;
                Vector3 dir = Quaternion.Euler(0f, 0f, angle) * normal;
                Quaternion rotation = Quaternion.FromToRotation(normal, dir);

                ReDraw.Capsule(origin + direction * hit.distance, rotation, size, Color.green.WithAlpha(0.5f), depthMode);

                ReDraw.Circle(hit.point, Vector3.back, DrawMode.BillboardFree, Size.Pixels(6f), FillMode.Fill, Color.blue, depthMode);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 1f, depthMode);
            }
        }

        /// <summary>
        /// Visualize an OverlapCapsuleAll2D
        /// </summary>
        /// <param name="origin">world position of capsule</param>
        /// <param name="size">size of capsule</param>
        /// <param name="angle">rotation of capsule on Z-Axis</param>
        /// <param name="layerMask">LayerMask to check against</param>
        public static void OverlapCapsule2D(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, int layerMask = ~0, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 normal = capsuleDirection == CapsuleDirection2D.Vertical ? Vector2.up : Vector2.right;
            Vector3 dir = Quaternion.Euler(0f, 0f, angle) * normal;
            Quaternion rotation = Quaternion.FromToRotation(normal, dir);

            var hits = Physics2D.OverlapCapsuleAll(origin, size, capsuleDirection, angle, layerMask);
            if (hits.Length > 0)
            {
                ReDraw.Capsule(origin, rotation, size, Color.green.WithAlpha(0.5f), depthMode);
                ReDraw.TextSDF($"{hits.Length}", origin, 12f, Color.white, depthMode);
            }
            else
            {
                ReDraw.Capsule(origin, rotation, size, Color.red.WithAlpha(0.5f), depthMode);
            }
        }
    }
}