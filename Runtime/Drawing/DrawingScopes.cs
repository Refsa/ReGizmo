using System;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public struct TransformScope : IDisposable
    {
        PositionScope positionScope;
        RotationScope rotationScope;
        ScaleScope scaleScope;

        public TransformScope(Transform transform)
        {
            positionScope = new PositionScope(transform.position);
            rotationScope = new RotationScope(transform.rotation);
            scaleScope = new ScaleScope(transform.lossyScale);
        }

        public void Dispose()
        {
            positionScope.Dispose();
            rotationScope.Dispose();
            scaleScope.Dispose();
        }
    }

    public struct LocalTransformScope : IDisposable
    {
        PositionScope positionScope;
        RotationScope rotationScope;
        ScaleScope scaleScope;

        public LocalTransformScope(Transform transform)
        {
            positionScope = new PositionScope(transform.localPosition);
            rotationScope = new RotationScope(transform.localRotation);
            scaleScope = new ScaleScope(transform.localScale);
        }

        public void Dispose()
        {
            positionScope.Dispose();
            rotationScope.Dispose();
            scaleScope.Dispose();
        }
    }

    public struct PositionScope : IDisposable
    {
        Vector3 oldPosition;

        public PositionScope(Vector3 position)
        {
            oldPosition = ReDraw.currentPosition;
            ReDraw.currentPosition = position;
        }

        public void Dispose()
        {
            ReDraw.currentPosition = oldPosition;
        }
    }

    public struct RotationScope : IDisposable
    {
        Quaternion oldRotation;

        public RotationScope(Quaternion rotation)
        {
            oldRotation = ReDraw.currentRotation;
            ReDraw.currentRotation = rotation;
        }

        public void Dispose()
        {
            ReDraw.currentRotation = oldRotation;
        }
    }

    public struct ScaleScope : IDisposable
    {
        Vector3 oldScale;

        public ScaleScope(Vector3 scale)
        {
            oldScale = ReDraw.currentScale;
            ReDraw.currentScale = scale;
        }

        public void Dispose()
        {
            ReDraw.currentScale = oldScale;
        }
    }

    public struct ColorScope : IDisposable
    {
        Color oldColor;

        public ColorScope(Color color)
        {
            oldColor = ReDraw.currentColor;
            ReDraw.currentColor = color;
        }

        public void Dispose()
        {
            ReDraw.currentColor = oldColor;
        }
    }
}