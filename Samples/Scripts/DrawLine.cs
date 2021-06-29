using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;

namespace ReGizmo.Samples
{
    [AddComponentMenu("ReGizmo Samples/Draw Line")]
    internal class DrawLine : DrawSampleBase
    {
        [SerializeField] Transform point1;
        [SerializeField] Transform point2;

        protected override void Draw()
        {
            ReDraw.Line(point1.position, point2.position, Color.blue, 5f, depthMode);
        }
    }
}