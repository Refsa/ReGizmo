using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public static class LineDataBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData Start()
        {
            return new PolyLineData();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData WithPosition(this PolyLineData self, Vector3 position)
        {
            self.Position = position;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData WithColor(this PolyLineData self, Color color)
        {
            self.Color = color;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData WithWidth(this PolyLineData self, float width)
        {
            self.Width = width;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData WithEdgeSmoothing(this PolyLineData self)
        {
            self.EdgeSmoothing = 1f;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolyLineData Copy(this PolyLineData self, PolyLineData other)
        {
            self.Color = other.Color;
            self.Position = other.Position;
            self.EdgeSmoothing = other.EdgeSmoothing;
            self.Width = other.Width;

            return self;
        }
    }
}