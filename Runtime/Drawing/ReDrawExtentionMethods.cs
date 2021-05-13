using UnityEngine;

namespace ReGizmo.Drawing.Ext
{
    public static class ReDrawExtensionMethods
    {
        /// <summary>
        /// Visualize a sphere cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="origin">world position of collider</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawSphereCast(this SphereCollider collider, Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.SphereCast(origin + collider.center, direction, collider.radius, distance, layerMask);
        }

        /// <summary>
        /// Visualize a sphere cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction of cast</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawSphereCast(this SphereCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.SphereCast(rigidbody.position + collider.center, direction, collider.radius, distance, layerMask);
        }

        /// <summary>
        /// Visualize a box cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="origin">world position of collider</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="rotation">rotation of collider</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawBoxCast(this BoxCollider collider, Vector3 origin, Vector3 direction, Quaternion rotation, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast(origin + collider.center, direction, collider.size, rotation, distance, layerMask);
        }

        /// <summary>
        /// Visualize a box cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction of cast</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawBoxCast(this BoxCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast(rigidbody.position + collider.center, direction, collider.size, rigidbody.rotation, distance, layerMask);
        }

        /// <summary>
        /// Visualize a capsule cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="center">world position of collider</param>
        /// <param name="direction">direction of cast</param>
        /// <param name="rotation">rotation of collider</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCapsuleCast(this CapsuleCollider collider, Vector3 center, Vector3 direction, Quaternion rotation, float distance = float.MaxValue, int layerMask = ~0)
        {
            Vector3 capsuleDir = rotation * Vector3.up;
            float halfHeight = collider.height * 0.5f;
            Vector3 p1 = center + (collider.center + capsuleDir * halfHeight);
            Vector3 p2 = center + (collider.center - capsuleDir * halfHeight);

            ReDraw.CapsuleCast(p1, p2, collider.radius, direction, distance, layerMask);
        }

        /// <summary>
        /// Visualize a capsule cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction to cast</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCapsuleCast(this CapsuleCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            Vector3 capsuleDir = rigidbody.rotation * Vector3.up;
            float halfHeight = collider.height * 0.5f;
            Vector3 p1 = rigidbody.position + (collider.center + capsuleDir * halfHeight);
            Vector3 p2 = rigidbody.position + (collider.center - capsuleDir * halfHeight);

            ReDraw.CapsuleCast(p1, p2, collider.radius, direction, distance, layerMask);
        }

        /// <summary>
        /// Visualize a 2D box cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="origin">world position of collider</param>
        /// <param name="angle">rotation of collider on Z-Axis</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawBoxCast2D(this BoxCollider2D collider, Vector2 origin, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast2D(origin + collider.offset, collider.size, angle, direction, distance, layerMask);
        }

        /// <summary>
        /// Visualize a 2D box cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawBoxCast2D(this BoxCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast2D(rigidbody.position + collider.offset, collider.size, rigidbody.rotation, direction, distance, layerMask);
        }

        /// <summary>
        /// Visualize a 2D circle cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="origin">world position of collider</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCircleCast2D(CircleCollider2D collider, Vector2 origin, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CircleCast2D(origin + collider.offset, collider.radius, direction, distance, layerMask);
        }

        /// <summary>
        /// Visuaize a 2D circle cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCircleCast2D(CircleCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CircleCast2D(rigidbody.position + collider.offset, collider.radius, direction, distance, layerMask);
        }

        /// <summary>
        /// Visualize a 2D capsule cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="origin">world position of collider</param>
        /// <param name="angle">rotation of collider on Z-Axis</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCapsuleCast2D(this CapsuleCollider2D collider, Vector2 origin, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CapsuleCast2D(origin + collider.offset, collider.size, collider.direction, angle, direction, distance);
        }

        /// <summary>
        /// Visualize a 2D capsule cast
        /// </summary>
        /// <param name="collider">collider to use</param>
        /// <param name="rigidbody">rigidbody to use</param>
        /// <param name="direction">direction to cast in</param>
        /// <param name="distance">max distance to cast</param>
        /// <param name="layerMask">layermask to test against</param>
        public static void DrawCapsuleCast2D(this CapsuleCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CapsuleCast2D(rigidbody.position + collider.offset, collider.size, collider.direction, rigidbody.rotation, direction, distance);
        }
    }
}