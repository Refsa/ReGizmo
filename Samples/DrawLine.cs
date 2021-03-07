﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;

public class DrawLine : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    void OnDrawGizmos() 
    {
        ReDraw.Line(point1.position, point2.position, Color.blue, 5f);
    }
}
