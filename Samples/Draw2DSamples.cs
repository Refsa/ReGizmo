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

        // Screen-Space
        {
            ReDraw.Circle(Vector3.zero, Vector3.zero, circleOuterRadus, circleInnerRadius, Color.red);
            ReDraw.Triangle(Vector3.right * 12f, Vector3.zero, circleOuterRadus, circleInnerRadius, Color.green);
        }

        // World-Space
        {
            ReDraw.Circle(Vector3.zero, Vector3.up, circleOuterRadus, circleInnerRadius, Color.green);
            ReDraw.Triangle(Vector3.right * 12f, Vector3.up, circleOuterRadus, circleInnerRadius, Color.red);
        }

        ReDraw.Line(Vector3.left * 12f, Vector3.left * 12f + Vector3.up * 3f, Color.red, 1f);
        ReDraw.Triangle(Vector3.left * 12f + Vector3.up * 3f, Vector3.up, 0.15f, 0f, Color.red);

        ReDraw.Line(Vector3.left * 12f, Vector3.left * 12f + Vector3.back * 3f, Color.red, 1f);
        ReDraw.Triangle(Vector3.left * 12f + Vector3.back * 3f, Vector3.back, 0.15f, 0f, Color.red);

        ReDraw.Line(Vector3.left * 14f, Vector3.left * 14f + Vector3.up * 3f, Color.red, 1f);

        var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
        Vector3 dir = ((Vector3.left * 14f + Vector3.up * 3f) - (Vector3.left * 14f)).normalized;
        //Vector3 viewDir = ((Vector3.left * 14f + Vector3.up * 3f) - camera.transform.position).normalized;
        //dir = Vector3.Cross(dir, viewDir);

        var ldir = camera.transform.TransformDirection(Quaternion.Euler(0f, 0f, 135f) * dir);
        var rdir = camera.transform.TransformDirection(Quaternion.Euler(0f, 0f, -135f) * dir);

        ReDraw.Ray(Vector3.left * 14f + Vector3.up * 3f, ldir * 0.15f, Color.red, 1f);
        ReDraw.Ray(Vector3.left * 14f + Vector3.up * 3f, rdir * 0.15f, Color.red, 1f);
    }
}
