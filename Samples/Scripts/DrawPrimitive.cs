using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class DrawPrimitive : MonoBehaviour
    {
        [SerializeField] Color color = Color.white;
        void OnDrawGizmos()
        {
            ReDraw.Cone(transform.position, transform.rotation, transform.localScale, color);
        }
    }
}