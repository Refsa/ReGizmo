using System;
using UnityEngine;

namespace ReGizmo.Runtime.Utils
{
    internal static class MathUtils
    {
        public static Vector2 Round(this in Vector2 self, int digits)
        {
            return new Vector2(
                (float) Math.Round(self.x, digits),
                (float) Math.Round(self.y, digits));
        }
    }
}