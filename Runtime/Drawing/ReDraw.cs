
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        internal static Vector3 currentPosition = Vector3.zero;
        internal static Quaternion currentRotation = Quaternion.identity;
        internal static Vector3 currentScale = Vector3.zero;
        internal static Color currentColor = Color.white;
    }
}