
using UnityEngine;
using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {

        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single length, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * length;
            ReDraw.Line(position, endPoint, color, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single length, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * length;
            ReDraw.Line(position, endPoint, currentColor, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single length, System.Single lineWidth, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * length;
            ReDraw.Line(position, endPoint, color, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, color, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single length, System.Single lineWidth, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * length;
            ReDraw.Line(position, endPoint, currentColor, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, ReGizmo.Drawing.Size arrowSize, System.Single lineWidth, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, currentColor, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, ReGizmo.Drawing.Size arrowSize, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, color, 1f);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single lineWidth, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, color, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, ReGizmo.Drawing.Size arrowSize, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, currentColor, 1f);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, arrowSize, 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * arrowSize.Value + Vector3.up * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * arrowSize.Value, currentColor, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, System.Single lineWidth, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, currentColor, lineWidth);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, UnityEngine.Color color, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, color, 1f);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, color, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, color, depthMode);
                    break;
            }
        }
        public static void Arrow(Vector3 position, Vector3 direction, ArrowCap cap, DepthMode depthMode = DepthMode.Sorted)
        {
            Vector3 endPoint = position + direction * 1f;
            ReDraw.Line(position, endPoint, currentColor, 1f);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, Size.Units(0.2f), 0f, currentColor, depthMode);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value + Vector3.up * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * Size.Units(0.2f).Value, currentColor, depthMode);
                    break;
            }
        }
    }
}