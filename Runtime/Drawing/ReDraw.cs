
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public enum DrawMode : int
    {
        BillboardFree = 1 << 0,
        BillboardAligned = 1 << 1,
        AxisAligned = 1 << 2,
    }

    public enum SizeMode : int
    {
        Pixel = 1 << 11,
        Percent = 1 << 12,
        Unit = 1 << 13,
    }

    public enum FillMode : int
    {
        Fill = 1 << 20,
        Outline = 1 << 21,
    }

    public struct Size
    {
        public int SizeMode;
        public float Value;

        public static Size Pixels(float pixels)
        {
            return new Size { SizeMode = (int)Drawing.SizeMode.Pixel, Value = pixels };
        }

        public static Size Percent(float percent)
        {
            return new Size { SizeMode = (int)Drawing.SizeMode.Percent, Value = Mathf.Clamp01(percent) };
        }

        public static Size Units(float units)
        {
            return new Size { SizeMode = (int)Drawing.SizeMode.Unit, Value = units };
        }
    }

    public enum ArrowCap : int
    {
        Triangle = 0,
        Cone,
        Sphere,
        Cube
    }

    /// <summary>
    /// Other partial implementations only contains extension methods for the different drawing calls
    /// </summary>
    public partial class ReDraw
    {
        static Vector3 V3One = Vector3.one;
        static Vector3 V3Zero = Vector3.zero;
        static Vector3 V3Back = Vector3.back;
        static Vector3 V3Forward = Vector3.forward;
        static Vector3 V3Up = Vector3.up;
        static Vector3 V3Right = Vector3.right;

        internal static Vector3 currentPosition = Vector3.zero;
        internal static Quaternion currentRotation = Quaternion.Euler(0f, 0f, 0f);
        internal static Vector3 currentScale = Vector3.zero;
        internal static Color currentColor = Color.white;
    }
}