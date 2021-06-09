using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal class DrawLines : MonoBehaviour
    {
        [SerializeField, Range(1f, 32f)] private float width = 1f;

        PolyLine cachedPolyLine;

        private void OnDrawGizmos()
        {
            using (new TransformScope(transform))
            {
                for (int i = 0; i < 32; i++)
                {
                    ReDraw.Ray(new Vector3(i, 0, 0), (Vector3.up + Vector3.right + Vector3.forward).normalized * (i + 1), Color.red, width + (i * 0.25f));
                    ReDraw.Ray(new Vector3(i, 0, 0), (Vector3.up).normalized * (i + 1), Color.blue, width + (i * 0.25f));
                }

                {
                    new PolyLine(false)
                        .Point(Vector3.back, Color.red, 1f)
                        .Point(Vector3.back * 2 + Vector3.up, Color.green, 1f)
                        .Point(Vector3.back * 4 + Vector3.up, Color.blue, 1f)
                        .Point(Vector3.back * 6, Color.cyan, 1f)
                        .ClosedLoop()
                        .Draw();

                    new PolyLine(false)
                        .Point(Vector3.right + Vector3.back, Color.red, 1f)
                        .Point(Vector3.right + Vector3.back * 2 + Vector3.up, Color.green, 2f)
                        .Point(Vector3.right + Vector3.back * 4 + Vector3.up, Color.blue, 4f)
                        .Point(Vector3.right + Vector3.back * 6, Color.cyan, 8f)
                        .Draw();

                    new PolyLine(false)
                        .Point(Vector3.right * 2 + Vector3.back, Color.red, 10f)
                        .Point(Vector3.right * 2 + Vector3.back * 2 + Vector3.up, Color.green, 20f)
                        .Point(Vector3.right * 2 + Vector3.back * 4 + Vector3.up, Color.blue, 30f)
                        .Point(Vector3.right * 2 + Vector3.back * 6, Color.cyan, 40f)
                        .Draw();

                    new PolyLine(false)
                        .Point(Vector3.right * 3 + Vector3.back, Color.red, 10f)
                        .Point(Vector3.right * 3 + Vector3.back + Vector3.up, Color.green, 20f)
                        .Point(Vector3.right * 3 + Vector3.back * 4 + Vector3.up, Color.blue, 30f)
                        .Draw();
                }

                /* {
                    Vector3 dir1 = ((Vector3.back * 2 + Vector3.up) - Vector3.back).normalized;
                    Vector3 dir2 = ((Vector3.back * 4 + Vector3.up) - (Vector3.back * 2 + Vector3.up)).normalized;

                    Vector3 cross1 = Vector3.Cross(dir1, UnityEditor.SceneView.lastActiveSceneView.camera.transform.forward);
                    Vector3 cross2 = Vector3.Cross(dir2, UnityEditor.SceneView.lastActiveSceneView.camera.transform.forward);
                    Vector3 cross = Vector3.Cross(cross1, cross2);

                    ReDraw.Ray((Vector3.back * 2 + Vector3.up), cross, Color.magenta, 2f);
                } */
            }


            {
                if (!cachedPolyLine.Initialized || cachedPolyLine.Count == 0)
                {
                    cachedPolyLine = new PolyLine(false).DontDispose();

                    Vector3 point = Random.insideUnitSphere * 0.001f;
                    float maxTheta = 10f;
                    float count = 1 << 10;

                    for (int i = 0; i < count; i++)
                    {
                        cachedPolyLine.Point(point, new Color(i / count, 1 - (i / count), Random.Range(0f, 1f), 1f), 1f);

                        Vector3 change = Random.insideUnitSphere * Random.Range(0.01f, maxTheta);

                        point = Quaternion.Euler(change) * point;
                        Vector3 dir = (transform.position - point).normalized;
                        point += dir * Random.Range(0.001f, 0.02f);
                    }
                }

                ReDraw.PolyLine(cachedPolyLine);
            }
        }
    }
}