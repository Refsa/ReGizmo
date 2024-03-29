using System.Runtime.CompilerServices;
using UnityEngine;

using V3 = System.Numerics.Vector3;

namespace ReGizmo.Utils
{
    public static class VectorExt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(this ref Vector3 self, in Vector3 other)
        {
            self.x += other.x;
            self.y += other.y;
            self.z += other.z;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Mul(this ref Vector3 self, in Vector3 other)
        {
            self.x *= other.x;
            self.y *= other.y;
            self.z *= other.z;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Mul(this ref Vector3 self, in float scalar)
        {
            self.x *= scalar;
            self.y *= scalar;
            self.z *= scalar;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAdd(this ref Vector3 self, in Vector3 a, in Vector3 b)
        {
            self.x = a.x + b.x;
            self.y = a.y + b.y;
            self.z = a.z + b.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this ref Vector3 self, in Vector3 from)
        {
            self.x = from.x;
            self.y = from.y;
            self.z = from.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this ref Vector4 self, in Quaternion quaternion)
        {
            self.x = quaternion.x;
            self.y = quaternion.y;
            self.z = quaternion.z;
            self.w = quaternion.w;
        }
    }
}