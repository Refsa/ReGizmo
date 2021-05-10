﻿using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class Draw2DSamples : MonoBehaviour
{
    [SerializeField, Range(1f, 1024f)] float circleOuterRadus = 128f;
    [SerializeField, Range(1f, 1024f)] float circleInnerRadius = 6f;
    [SerializeField, Range(0f, 360f)] float rotation;

    [SerializeField] bool regen;

    List<Vector3> dirs;

    void OnDrawGizmos()
    {
        if (regen)
        {
            dirs = null;
            regen = false;
        }

        // Screen-Space
        {
            ReDraw.Circle(Vector3.zero, Vector3.zero, DrawMode.BillboardFree, Size.Pixels(circleOuterRadus * 40f), circleInnerRadius, Color.red);

            ReDraw.Triangle(Vector3.right * 12f, Vector3.zero, DrawMode.BillboardFree, Size.Pixels(circleOuterRadus * 40f), circleInnerRadius * 40f, Color.green);
        }

        // World-Space
        {
            ReDraw.Circle(Vector3.zero, Vector3.up, DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.green);
            ReDraw.Circle(Vector3.zero, Vector3.right, DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.red);
            ReDraw.Circle(Vector3.zero, Vector3.forward, DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.blue);

            float rotationX = 180f * Mathf.Sin((float)UnityEditor.EditorApplication.timeSinceStartup * 0.2f);
            float rotationZ = 180f * Mathf.Cos((float)UnityEditor.EditorApplication.timeSinceStartup * 0.2f);
            //float rotationX = rotation;
            //float rotationZ = 360f - rotationX;

            ReDraw.Circle(Vector3.back * 15f, (Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.up), DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.green);
            ReDraw.Circle(Vector3.back * 15f, (Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.right), DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.red);
            ReDraw.Circle(Vector3.back * 15f, (Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.forward), DrawMode.AxisAligned, Size.Units(circleOuterRadus), circleInnerRadius, Color.blue);

            ReDraw.Ray(Vector3.back * 15f, Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.up * circleOuterRadus, Color.green, 1f);
            ReDraw.Ray(Vector3.back * 15f, Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.right * circleOuterRadus, Color.red, 1f);
            ReDraw.Ray(Vector3.back * 15f, Quaternion.Euler(rotationX, 0f, rotationZ) * Vector3.forward * circleOuterRadus, Color.blue, 1f);

            ReDraw.Triangle(Vector3.right * 12f, Vector3.up, DrawMode.AxisAligned, Size.Pixels(circleOuterRadus), circleInnerRadius, Color.red);
        }

        if (dirs == null || dirs.Count != 64)
        {
            dirs = new List<Vector3>();
            for (int i = 0; i < 64; i++)
            {
                dirs.Add(Random.insideUnitSphere);
            }
        }

        Vector3 center = Vector3.left * 12f;
        for (int i = 0; i < 64; i++)
        {
            Vector3 arrowDir = dirs[i];

            ReDraw.Line(center, center + arrowDir * 3f, Color.red, 1f);
            ReDraw.Triangle(center + arrowDir * 3f, arrowDir, DrawMode.BillboardAligned, Size.Pixels(8f), 0f, Color.red);
        }

        ReDraw.Line(Vector3.left * 14f, Vector3.left * 14f + Vector3.up * 3f, Color.red, 1f);

        var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
        Vector3 dir = ((Vector3.left * 14f + Vector3.up * 3f) - (Vector3.left * 14f)).normalized;
        var ldir = camera.transform.TransformDirection(Quaternion.Euler(0f, 0f, 135f) * dir);
        var rdir = camera.transform.TransformDirection(Quaternion.Euler(0f, 0f, -135f) * dir);
        ReDraw.Ray(Vector3.left * 14f + Vector3.up * 3f, ldir * 0.15f, Color.red, 1f);
        ReDraw.Ray(Vector3.left * 14f + Vector3.up * 3f, rdir * 0.15f, Color.red, 1f);
    }
}
