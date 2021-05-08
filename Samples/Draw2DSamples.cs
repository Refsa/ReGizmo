using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class Draw2DSamples : MonoBehaviour
{
    [SerializeField, Range(1f, 1024f)] float circleOuterRadus = 128f;
    [SerializeField, Range(1f, 1024f)] float circleInnerRadius = 6f;

    void OnDrawGizmos()
    {
        ReDraw.Circle(Vector3.zero, Vector3.up, circleOuterRadus, circleInnerRadius, Color.red);

        ReDraw.Triangle(Vector3.right * 5f, Vector3.up, circleOuterRadus, 0f, Color.green);
    }
}
