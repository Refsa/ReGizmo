using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class DrawGrid : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            ReDraw.Grid(transform.position, Vector2.one, transform.up, 1f, Color.white);
        }
    }
}