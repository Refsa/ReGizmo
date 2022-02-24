using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class GeneratedMeshTest : MonoBehaviour
    {
        [SerializeField] float hAngle = 45f;
        [SerializeField] float vAngle = 45f;
        [SerializeField] float baseSize = 0.1f;
        [SerializeField, Range(1, 16)] int resolution = 4;
        [SerializeField] Vector2 range;

        Mesh mesh;

        void OnValidate()
        {
            GenerateMesh();
        }

        void OnDrawGizmos()
        {
            if (mesh != null)
            {
                ReDraw.Mesh(mesh, transform.position, transform.rotation, Vector3.one, ReColors.BLUE_GREEN.WithAlpha(0.2f));
                ReDraw.WireframeMesh(mesh, transform.position, transform.rotation, Vector3.one, ReColors.YELLOW);
            }
        }

        void GenerateMesh()
        {
            var points = new Vector3[4 + (2 + resolution) * (2 + resolution)];

            var baseTriangleIndices = 2 * 3;
            var arcTriangleIndices = (resolution + 1) * (resolution + 1) * 2 * 3;
            var sideTriangleIndices = (resolution + 2) * 3;
            var tris = new int[baseTriangleIndices + arcTriangleIndices + sideTriangleIndices * 4];

            // Base points
            points[0] = new Vector3(-baseSize / 2f, -baseSize / 2f, range.x); // Bottom Left
            points[1] = new Vector3(baseSize / 2f, -baseSize / 2f, range.x);  // Bottom Right
            points[2] = new Vector3(baseSize / 2f, baseSize / 2f, range.x);   // Top Right
            points[3] = new Vector3(-baseSize / 2f, baseSize / 2f, range.x);  // Top Left
            // points[0] = Quaternion.Euler(vAngle, hAngle, 0f) * Vector3.forward * range.Start;
            tris[0] = 2; tris[1] = 1; tris[2] = 0; tris[3] = 3; tris[4] = 2; tris[5] = 0;

            for (int y = 0; y < 2 + resolution; y++)
            {
                for (int x = 0; x < 2 + resolution; x++)
                {
                    int i = 4 + y * (2 + resolution) + x;
                    float ay = Mathf.Lerp(-hAngle / 2f, hAngle / 2f, (float)x / (float)(resolution + 1));
                    float ax = Mathf.Lerp(-vAngle / 2f, vAngle / 2f, (float)y / (float)(resolution + 1));
                    Vector3 p = Quaternion.Euler(ax, ay, 0f) * Vector3.forward * range.y;
                    points[i] = p;

                    if (x < (1 + resolution) && y < (1 + resolution))
                    {
                        var ti = baseTriangleIndices + (y * (resolution + 1) + x) * 3 * 2;
                        tris[ti] = i + 1 + (2 + resolution); // top right
                        tris[ti + 1] = i + 1; // bottom right
                        tris[ti + 2] = i; // bottom left
                        tris[ti + 3] = i + (2 + resolution); // top left
                        tris[ti + 4] = i + (2 + resolution) + 1; // top right
                        tris[ti + 5] = i; // bottom left
                    }
                }
            }

            // Top and bottom side triangles
            for (int x = 0; x < 2 + resolution; x++)
            {
                var iTop = 4 + x;
                var iBottom = 4 + (1 + resolution) * (2 + resolution) + x;

                var tiTop = baseTriangleIndices + arcTriangleIndices + x * 3;
                var tiBottom = tiTop + sideTriangleIndices;
                if (x == 0)
                {
                    tris[tiTop] = 2;
                    tris[tiTop + 1] = 3;
                    tris[tiTop + 2] = iTop;

                    tris[tiBottom] = 0;
                    tris[tiBottom + 1] = 1;
                    tris[tiBottom + 2] = iBottom;
                }
                else
                {
                    tris[tiTop] = iTop;
                    tris[tiTop + 1] = 2;
                    tris[tiTop + 2] = iTop - 1;

                    tris[tiBottom] = 1;
                    tris[tiBottom + 1] = iBottom;
                    tris[tiBottom + 2] = iBottom - 1;
                }
            }

            var yIncr = 2 + resolution;
            for (int y = 0; y < 2 + resolution; y++)
            {
                var iLeft = 4 + y * (2 + resolution);
                var iRight = iLeft + (1 + resolution);

                var tiLeft = baseTriangleIndices + arcTriangleIndices + sideTriangleIndices * 2 + y * 3;
                var tiRight = tiLeft + sideTriangleIndices;
                if (y == 0)
                {
                    tris[tiLeft] = 3;
                    tris[tiLeft + 1] = 0;
                    tris[tiLeft + 2] = iLeft;

                    tris[tiRight] = 1;
                    tris[tiRight + 1] = 2;
                    tris[tiRight + 2] = iRight;
                }
                else
                {
                    tris[tiLeft] = 0;
                    tris[tiLeft + 1] = iLeft;
                    tris[tiLeft + 2] = iLeft - yIncr;

                    tris[tiRight] = iRight;
                    tris[tiRight + 1] = 1;
                    tris[tiRight + 2] = iRight - yIncr;
                }
            }

            mesh = new Mesh();
            mesh.vertices = points;
            mesh.triangles = tris;
            mesh.name = "VisionCode";
        }
    }
}