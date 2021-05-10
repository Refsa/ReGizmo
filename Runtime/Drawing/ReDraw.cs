
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public enum DrawMode : byte
    {
        BillboardFree,
        BillboardAligned,
        AxisAligned,
    }

    internal enum ScaleType : byte
    {
        Pixel = 0,
        Percent = 1,
        Unit = 2,
    }

    public struct Scale
    {
        public byte ScaleMode;
        public float Value;

        public static Scale Pixels(float pixels)
        {
            return new Scale { ScaleMode = (byte)ScaleType.Pixel, Value = pixels };
        }

        public static Scale Percent(float percent)
        {
            return new Scale { ScaleMode = (byte)ScaleType.Percent, Value = Mathf.Clamp01(percent) };
        }

        public static Scale Units(float units)
        {
            return new Scale { ScaleMode = (byte)ScaleType.Unit, Value = units };
        }
    }

    /// <summary>
    /// Other partial implementations only contains extension methods for the different drawing calls
    /// </summary>
    public partial class ReDraw
    {
        internal static Vector3 currentPosition = Vector3.zero;
        internal static Quaternion currentRotation = Quaternion.identity;
        internal static Vector3 currentScale = Vector3.zero;
        internal static Color currentColor = Color.white;
    }
}