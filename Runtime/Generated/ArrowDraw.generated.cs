
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Arrow(Vector3 position, Vector3 direction, System.Single length, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * length, color, lineWidth);
            ReDraw.Triangle(position + direction * length, direction, DrawMode.BillboardAligned, arrowSize, 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction, System.Single length, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth)
        {
            ReDraw.Line(position, position + direction * length, currentColor, lineWidth);
            ReDraw.Triangle(position + direction * length, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor);
        }
        public static void Arrow(Vector3 position, Vector3 direction, System.Single length, System.Single lineWidth, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * length, color, lineWidth);
            ReDraw.Triangle(position + direction * length, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * 1f, color, lineWidth);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, arrowSize, 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction, System.Single length, System.Single lineWidth)
        {
            ReDraw.Line(position, position + direction * length, currentColor, lineWidth);
            ReDraw.Triangle(position + direction * length, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, currentColor);
        }
        public static void Arrow(Vector3 position, Vector3 direction, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth)
        {
            ReDraw.Line(position, position + direction * 1f, currentColor, lineWidth);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor);
        }
        public static void Arrow(Vector3 position, Vector3 direction, ReGizmo.Drawing.Size arrowSize, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * 1f, color, 1f);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, arrowSize, 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction, System.Single lineWidth, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * 1f, color, lineWidth);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction, ReGizmo.Drawing.Size arrowSize)
        {
            ReDraw.Line(position, position + direction * 1f, currentColor, 1f);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor);
        }
        public static void Arrow(Vector3 position, Vector3 direction, System.Single lineWidth)
        {
            ReDraw.Line(position, position + direction * 1f, currentColor, lineWidth);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, currentColor);
        }
        public static void Arrow(Vector3 position, Vector3 direction, UnityEngine.Color color)
        {
            ReDraw.Line(position, position + direction * 1f, color, 1f);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, color);
        }
        public static void Arrow(Vector3 position, Vector3 direction)
        {
            ReDraw.Line(position, position + direction * 1f, currentColor, 1f);
            ReDraw.Triangle(position + direction * 1f, direction, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, currentColor);
        }
    }
}