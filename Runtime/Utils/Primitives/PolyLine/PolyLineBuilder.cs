using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo
{
    public static class PolyLineBuilder
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine ClosedLoop(this PolyLine self)
        {
            self.Looping = 1;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine DontDispose(this PolyLine self)
        {
            self.AutoDispose = false;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLine WithLine(this PolyLine self, LineData line)
        {
            self.Add(line);
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(this PolyLine self)
        {
            // ReGizmo.DrawPolyLine(ref self, self.AutoDispose);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PolyLine Build(this PolyLine self)
        {
            self.SetLastID();
            self.SetStartID();

            return self;
        }
    }
}