using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [SerializeField, Range(1f, 32f)] private float width = 1f;

    PolyLine cachedPolyLine;

    private void OnDrawGizmos()
    {
        using (new TransformScope(transform))
        {
            for (int i = 0; i < 32; i++)
            {
                ReDraw.Ray(new Vector3(i, 0, 0), (Vector3.up + Vector3.right + Vector3.forward).normalized * (i + 1), Color.red, width);
                ReDraw.Ray(new Vector3(i, 0, 0), (Vector3.up).normalized * (i + 1), Color.blue, width);
            }

            {
                new PolyLine(false)
                    .Point(Vector3.back, Color.red, 1f)
                    .Point(Vector3.back * 2 + Vector3.up, Color.green, 1f)
                    .Point(Vector3.back * 4 + Vector3.up, Color.blue, 1f)
                    .Point(Vector3.back * 6, Color.cyan, 1f)
                    .Draw();

                /* using (var polyLine = PolyLine.Scope())
                {
                    polyLine
                        .Point(Vector3.right + Vector3.back, Color.red, 1f)
                        .Point(Vector3.right + Vector3.back * 2 + Vector3.up, Color.green, 2f)
                        .Point(Vector3.right + Vector3.back * 4 + Vector3.up, Color.blue, 4f)
                        .Point(Vector3.right + Vector3.back * 6, Color.cyan, 8f);
                } */
            }

            /* {
                if (cachedPolyLine.Points == null || cachedPolyLine.Points.Count == 0)
                {
                    cachedPolyLine.AutoDispose = false;
                    Vector3 point = Random.insideUnitSphere * 3f;
                    float maxTheta = 10f;

                    for (int i = 0; i < 512; i++)
                    {
                        cachedPolyLine.Add(new PolyLineData(point, new Color(i / 512f, 1 - (i / 512f), Random.Range(0f, 1f), 1f), 1f));

                        Vector3 change = Random.insideUnitSphere * maxTheta;
                        point = Quaternion.Euler(change) * point;
                    }
                }

                ReDraw.PolyLine(cachedPolyLine);
            } */
        }
    }
}
