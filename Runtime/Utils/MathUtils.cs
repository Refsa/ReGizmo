using System;
using UnityEngine;

namespace ReGizmo.Utils
{
    internal static class MathUtils
    {
        public static Vector2 Round(this in Vector2 self, int digits)
        {
            return new Vector2(
                (float) Math.Round(self.x, digits),
                (float) Math.Round(self.y, digits));
        }

        public static Vector2 ToVector2(this in Vector3 self)
        {
            return new Vector2(self.x, self.y);
        }

        public static Vector3 ToVector3(this in Vector2 self)
        {
            return new Vector3(self.x, self.y, 0f);
        }

        public static Vector4 ToVector4(this in Quaternion self)
        {
            return new Vector4(self.x, self.y, self.z, self.w);
        }
    }
}