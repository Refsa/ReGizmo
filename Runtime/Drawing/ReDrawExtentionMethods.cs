using UnityEngine;

namespace ReGizmo.Drawing.Ext
{
    public static class ReDrawExtensionMethods
    {
        public static void SphereCast(this SphereCollider collider, Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.SphereCast(origin + collider.center, direction, collider.radius, distance, layerMask);
        }

        public static void BoxCast(this BoxCollider collider, Vector3 origin, Vector3 direction, Quaternion rotation, float distance = float.MaxValue, int layerMask = ~0)
        {
            ReDraw.BoxCast(origin + collider.center, direction, collider.size, rotation, distance, layerMask);
        }
    }
}