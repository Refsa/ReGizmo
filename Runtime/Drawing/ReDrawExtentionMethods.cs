using UnityEngine;

namespace ReGizmo.Drawing.Ext
{
    public static class ReDrawExtensionMethods
    {
        public static void SphereCast(this SphereCollider collider, Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.SphereCast(origin + collider.center, direction, collider.radius, distance, layerMask);
        }

        public static void SphereCast(this SphereCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.SphereCast(rigidbody.position + collider.center, direction, collider.radius, distance, layerMask);
        }

        public static void BoxCast(this BoxCollider collider, Vector3 origin, Vector3 direction, Quaternion rotation, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast(origin + collider.center, direction, collider.size, rotation, distance, layerMask);
        }

        public static void BoxCast(this BoxCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast(rigidbody.position + collider.center, direction, collider.size, rigidbody.rotation, distance, layerMask);
        }

        public static void CapsuleCast(this CapsuleCollider collider, Vector3 center, Vector3 direction, Quaternion rotation, float distance = float.MaxValue, int layerMask = ~0)
        {
            Vector3 capsuleDir = rotation * Vector3.up;
            float halfHeight = collider.height * 0.5f;
            Vector3 p1 = center + (collider.center + capsuleDir * halfHeight);
            Vector3 p2 = center + (collider.center - capsuleDir * halfHeight);

            ReDraw.CapsuleCast(p1, p2, collider.radius, direction, distance, layerMask);
        }

        public static void CapsuleCast(this CapsuleCollider collider, Rigidbody rigidbody, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            Vector3 capsuleDir = rigidbody.rotation * Vector3.up;
            float halfHeight = collider.height * 0.5f;
            Vector3 p1 = rigidbody.position + (collider.center + capsuleDir * halfHeight);
            Vector3 p2 = rigidbody.position + (collider.center - capsuleDir * halfHeight);

            ReDraw.CapsuleCast(p1, p2, collider.radius, direction, distance, layerMask);
        }

        public static void BoxCast2D(this BoxCollider2D collider, Vector2 origin, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast2D(origin + collider.offset, collider.size, angle, direction, distance, layerMask);
        }

        public static void BoxCast2D(this BoxCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast2D(rigidbody.position + collider.offset, collider.size, rigidbody.rotation, direction, distance, layerMask);
        }

        public static void CircleCast2D(CircleCollider2D collider, Vector2 origin, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CircleCast2D(origin + collider.offset, collider.radius, direction, distance, layerMask);
        }

        public static void CircleCast2D(CircleCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CircleCast2D(rigidbody.position + collider.offset, collider.radius, direction, distance, layerMask);
        }

        public static void CapsuleCast2D(this CapsuleCollider2D collider, Vector2 origin, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CapsuleCast2D(origin + collider.offset, collider.size, collider.direction, angle, direction, distance);
        }

        public static void CapsuleCast2D(this CapsuleCollider2D collider, Rigidbody2D rigidbody, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.CapsuleCast2D(rigidbody.position + collider.offset, collider.size, collider.direction, rigidbody.rotation, direction, distance);
        }
    }
}