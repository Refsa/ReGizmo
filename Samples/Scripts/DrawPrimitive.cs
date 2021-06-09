using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class DrawPrimitive : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            /* using (new TransformScope(transform))
            {
                ReDraw.Cone(Color.red);
            } */

            ReDraw.Cone(transform.position, transform.rotation, transform.localScale, Color.red);
        }
    }
}