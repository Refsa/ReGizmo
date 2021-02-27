using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo
{
    public static class LineDataBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData Start()
        {
            return new LineData();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData WithPosition(this LineData self, Vector3 position)
        {
            self.Position = position;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData WithColor(this LineData self, Color color)
        {
            self.Color = color;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData WithWidth(this LineData self, float width)
        {
            self.Width = width;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData WithEdgeSmoothing(this LineData self)
        {
            self.EdgeSmoothing = 1f;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineData Copy(this LineData self, LineData other)
        {
            self.Color = other.Color;
            self.Position = other.Position;
            self.EdgeSmoothing = other.EdgeSmoothing;
            self.Width = other.Width;

            return self;
        }
    }
}