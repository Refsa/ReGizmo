using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public static class PolyLineBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine Point(this PolyLine self, Vector3 position, Vector4 color, float width = 1f)
        {
            self.Points.Add(new PolyLineData(position, color, width, self.ID));
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine ClosedLoop(this PolyLine self)
        {
            self.Looping = true;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine DontDispose(this PolyLine self)
        {
            self.AutoDispose = false;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine WithDepthMode(this PolyLine self, DepthMode depthMode)
        {
            self.depthMode = depthMode;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(this PolyLine self)
        {
            ReDraw.PolyLine(self);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PolyLine Build(this PolyLine self)
        {
            if (self.Looping)
            {
                self.Points.Add(self.Points[0]);
            }

            return self;
        }
    }
}