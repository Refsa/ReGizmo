using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine.Profiling;

namespace ReGizmo.Samples
{
    [AddComponentMenu("ReGizmo Samples/Draw Sample")]
    internal class DrawSample : MonoBehaviour
    {
        [SerializeField] Mesh someMesh;

        [SerializeField] Mesh[] customMeshes;
        [SerializeField] Texture2D[] customIcons;

        Color[] colors = new Color[] { Color.red, Color.green, Color.blue };

        Vector3[] someMeshVertices;
        int[] someMeshIndices;
        List<Vector3> arrowDirs;


#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Draw();
        }
#else
    void Update()
    {
        Draw();
    }
#endif

        void Draw()
        {
            float time = Time.time;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                time = (float)EditorApplication.timeSinceStartup;
            }
#endif

            Color cubeColor = Color.black;
            cubeColor.a = 1f;
            for (int i = 0; i < 16; i++)
            {
                cubeColor.r = (float)i / 16f;
                for (int j = 0; j < 16; j++)
                {
                    cubeColor.g = (float)j / 16f;

                    Vector3 pos = new Vector3(10 + i * 2, 0f, 10 + j * 2);
                    Quaternion rot = Quaternion.Euler(
                                    (i * Mathf.Sin(time) * 5.625f),
                                    0f,
                                    (j * Mathf.Sin(time) * 5.625f));

                    switch (i)
                    {
                        case 0:
                            ReDraw.Sphere(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 1:
                            ReDraw.Octahedron(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 2:
                            ReDraw.Pyramid(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 3:
                            ReDraw.Icosahedron(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 4:
                            ReDraw.Capsule(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 5:
                            ReDraw.Cylinder(pos, rot, Vector3.one, cubeColor);
                            break;
                        case 6:
                            ReDraw.Cone(pos, rot, Vector3.one, cubeColor);
                            break;
                        default:
                            ReDraw.Cube(pos, rot, Vector3.one, cubeColor);
                            break;
                    }
                }
            }

            // Primitives
            {
                ReDraw.Quad(Vector3.up * 5f + Vector3.back * 20f, Vector3.one * 2f, Color.red);
                ReDraw.Cube(Vector3.up * 5f + Vector3.back * 25f, Vector3.one * 2f, Color.red);
                ReDraw.Cylinder(Vector3.up * 5f + Vector3.back * 30f, Vector3.one * 2f, Color.red);
                ReDraw.Icosahedron(Vector3.up * 5f + Vector3.back * 35f, Vector3.one * 2f, Color.red);
                ReDraw.Octahedron(Vector3.up * 5f + Vector3.back * 40f, Vector3.one * 2f, Color.red);
                ReDraw.Pyramid(Vector3.up * 5f + Vector3.back * 45f, Vector3.one * 2f, Color.red);
                ReDraw.Sphere(Vector3.up * 5f + Vector3.back * 50f, Vector3.one * 2f, Color.red);
                ReDraw.Capsule(Vector3.up * 5f + Vector3.back * 55f, Vector3.one * 2f, Color.red);
            }

            if (customMeshes != null)
            {
                int index = 1;
                foreach (var cm in customMeshes)
                {
                    ReDraw.Mesh(cm, Vector3.up * index * 5, Quaternion.identity, Vector3.one, colors[index % 3]);

                    ReDraw.WireframeMesh(cm, Vector3.up * index * 5 + Vector3.forward * 5f, Quaternion.identity,
                        Vector3.one, colors[index % 3]);

                    ReDraw.Mesh(cm, Vector3.up * index * 5 + Vector3.forward * 10f, Quaternion.identity, Vector3.one,
                        colors[index % 3]);

                    ReDraw.WireframeMesh(cm, Vector3.up * index * 5 + Vector3.forward * 10f, Quaternion.identity,
                        Vector3.one, Color.black);
                    index++;
                }
            }

            // Text
            {
                string text = "Hello From ReGizmo";

                ReDraw.Text(text, Vector3.back * 5, 16f, Color.green);
                ReDraw.TextSDF(text, Vector3.back * 7, 16f, Color.green);

                text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890!#{}[]()";

                ReDraw.TextSDF("SDF: " + text, Vector3.up * 10f + Vector3.right * 15f, 16f, Color.blue);
                ReDraw.Text("REG: " + text, Vector3.up * 8f + Vector3.right * 15f, 16f, Color.blue);
            }

            // Lines
            {
                ReDraw.Line(Vector3.left, Vector3.left * 10, Color.cyan, Color.green, 5f, 5f);
                ReDraw.Line(Vector3.up + Vector3.left, Vector3.up + Vector3.left * 10, Color.cyan, Color.green, 5f, 5f);
            }

            // Poly-Lines
            {

            }

            // Ray
            {
                ReDraw.Ray(Vector3.left * 12f, Vector3.up, Color.green);
                ReDraw.Ray(Vector3.left * 12f, Vector3.right, Color.red);
                ReDraw.Ray(Vector3.left * 12f, Vector3.forward, Color.blue);
                ReDraw.Ray(Vector3.left * 12f, (Vector3.up + Vector3.right + Vector3.forward).normalized, Color.yellow);
            }

            // Raycast
            {
                ReDraw.Raycast(Vector3.up * 20f, Vector3.right, 100f);
                ReDraw.Raycast(Vector3.up * 20.5f, Vector3.right, 100f);
                ReDraw.Raycast(Vector3.up * 21f, Vector3.right, 100f);
                ReDraw.TextSDF("Raycast", Vector3.up * 21.1f, 12f, Color.white);
            }

            // Spherecast
            {
                float radius = 0.5f;
                ReDraw.SphereCast(Vector3.up * 20f + Vector3.forward * 5f, Vector3.right, radius, 100f);
                ReDraw.SphereCast(Vector3.up * 21f + Vector3.forward * 5f, Vector3.right, radius, 100f);
                ReDraw.SphereCast(Vector3.up * 22f + Vector3.forward * 5f, Vector3.right, radius, 100f);
                ReDraw.TextSDF("Spherecast", Vector3.up * 22.1f + Vector3.forward * 5f, 12, Color.white);
            }

            // Overlap Sphere
            {
                float radius = 1f;
                Vector3 origin = new Vector3(12f, 20f, 5f);
                ReDraw.OverlapSphere(origin, radius);
            }

            // Boxcast
            {
                Vector3 origin = Vector3.up * 20f + Vector3.forward * 10f;
                Vector3 size = Vector3.one * 0.5f;
                ReDraw.BoxCast(origin, Vector3.right, size, Quaternion.identity, 100f);
                ReDraw.BoxCast(origin + Vector3.up, Vector3.right, size, Quaternion.Euler(45f, 45f, 0f), 100f);
                ReDraw.BoxCast(origin + Vector3.up * 2f, Vector3.right, size, Quaternion.Euler(0f, 45f, 45f), 100f);
                ReDraw.TextSDF("Boxcast", origin + Vector3.up * 2.1f, 12, Color.white);
            }

            // Overlap Box
            {
                Vector3 size = Vector3.one * 2f;
                Vector3 origin = new Vector3(12.5f, 20f, 10f);
                ReDraw.OverlapBox(origin, size, Quaternion.identity);
            }

            // Capsulecast
            {
                float radius = 0.5f;
                Vector3 p1 = Vector3.up * 20f + Vector3.forward * 15f;
                ReDraw.CapsuleCast(p1, p1 + Vector3.up + Vector3.left, radius, Vector3.right, 100f);
                ReDraw.CapsuleCast(p1 + Vector3.up, p1 + Vector3.up * 2f + Vector3.left, radius, Vector3.right, 100f);
                ReDraw.CapsuleCast(p1 + Vector3.up * 2f, p1 + Vector3.up * 3f + Vector3.left, radius, Vector3.right, 100f);
                ReDraw.TextSDF("Capsulecast", Vector3.up * 22.1f + Vector3.forward * 15f, 12f, Color.white);
            }

            // Overlap Capsule
            {
                Vector3 size = Vector3.one * 2f;
                Vector3 p1 = Vector3.right * 12f + Vector3.up * 20f + Vector3.forward * 15f;
                Vector3 p2 = Vector3.right * 12f + Vector3.up * 20f + Vector3.forward * 15f + Vector3.right * 2f;
                ReDraw.OverlapCapsule(p1, p2, 1f);
            }

            // Raycast 2D
            {
                float dist = 20f;
                Vector2 startPoint = Vector3.up * 28f;

                ReDraw.Raycast2D(startPoint, Vector2.right, dist);
                ReDraw.Raycast2D(startPoint + Vector2.up * 0.5f, Vector2.right, dist);
                ReDraw.Raycast2D(startPoint + Vector2.up * 1f, Vector2.right, dist);
                ReDraw.TextSDF("Raycast2D", startPoint + Vector2.up * 1f, 12f, Color.white);
            }

            // BoxCast 2D
            {
                float dist = 20f;
                Vector2 startPoint = Vector3.up * 30f;

                ReDraw.BoxCast2D(startPoint, Vector2.one * 0.25f, 15f, Vector2.right, 20f);
                ReDraw.BoxCast2D(startPoint + Vector2.up * 0.5f, Vector2.one * 0.25f, 30f, Vector2.right, 20f);
                ReDraw.BoxCast2D(startPoint + Vector2.up * 1f, Vector2.one * 0.25f, 45f, Vector2.right, 20f);
                ReDraw.TextSDF("BoxCast2D", startPoint + Vector2.up * 1f, 12f, Color.white);
            }

            // Overlap Box 2D
            {
                Vector2 origin = new Vector2(4.25f, 30.25f);
                ReDraw.OverlapBox2D(origin, Vector2.one * 0.5f, 12.5f);
            }

            // CircleCast 2D
            {
                float dist = 20f;
                Vector2 startPoint = Vector3.up * 32f;

                ReDraw.CircleCast2D(startPoint, 0.25f, Vector2.right, 20f);
                ReDraw.CircleCast2D(startPoint + Vector2.up * 0.5f, 0.25f, Vector2.right, 20f);
                ReDraw.CircleCast2D(startPoint + Vector2.up * 1f, 0.25f, Vector2.right, 20f);
                ReDraw.TextSDF("CircleCast2D", startPoint + Vector2.up * 1f, 12f, Color.white);
            }

            // OVerlap Cirlce 2D
            {
                Vector2 origin = new Vector2(4.25f, 32.25f);
                ReDraw.OverlapCircle2D(origin, 0.3f);
            }

            // CapsuleCast 2D
            {
                float dist = 20f;
                Vector2 startPoint = Vector3.up * 34f;

                ReDraw.CapsuleCast2D(startPoint, Vector2.one * 0.25f, CapsuleDirection2D.Horizontal, 0f, Vector2.right, 20f);
                ReDraw.CapsuleCast2D(startPoint + Vector2.up * 0.5f, Vector2.one * 0.25f, CapsuleDirection2D.Horizontal, 30f, Vector2.right, 20f);
                ReDraw.CapsuleCast2D(startPoint + Vector2.up * 1f, Vector2.one * 0.25f, CapsuleDirection2D.Horizontal, 45f, Vector2.right, 20f);
                ReDraw.TextSDF("CapsuleCast2D", startPoint + Vector2.up * 1f, 12f, Color.white);
            }

            // Overlap Capsule 2D
            {
                Vector2 center = new Vector2(4.25f, 34.25f);
                ReDraw.OverlapCapsule2D(center, new Vector2(0.25f, 0.5f), CapsuleDirection2D.Vertical, -15f);
            }

            // Custom Icons
            {
                for (int i = 0; i < customIcons.Length; i++)
                {
                    ReDraw.Icon(customIcons[i], Vector3.left * 5 + Vector3.up * 2 * i, Color.black, Size.Pixels(32f));
                }
            }

            // Scopes
            {
                using (new TransformScope(transform))
                {
                    ReDraw.Sphere(Color.cyan);
                    ReDraw.Sphere(Vector3.back * 1, Color.cyan.WithAlpha(0.8f));
                    ReDraw.Sphere(Vector3.back * 2, Color.cyan.WithAlpha(0.6f));
                    ReDraw.Sphere(Vector3.back * 3, Color.cyan.WithAlpha(0.4f));
                }
            }

            // Dots and lines
            if (someMesh != null)
            {
                if (someMeshVertices == null || someMeshVertices.Length != someMesh.vertexCount)
                {
                    someMeshVertices = someMesh.vertices;
                    someMeshIndices = someMesh.GetIndices(0);
                }

                using (new PositionScope(Vector3.right * 10f))
                {
                    foreach (var vertex in someMeshVertices)
                    {
                        ReDraw.Sphere(vertex, Vector3.one * 0.01f, Color.red);
                    }

                    for (int i = 0; i < someMeshIndices.Length; i += 3)
                    {
                        Vector3 p1 = someMeshVertices[someMeshIndices[i]];
                        Vector3 p2 = someMeshVertices[someMeshIndices[i + 1]];
                        Vector3 p3 = someMeshVertices[someMeshIndices[i + 2]];

                        ReDraw.Line(p1, p2, Color.green, 2f);
                        ReDraw.Line(p2, p3, Color.green, 2f);
                        ReDraw.Line(p3, p1, Color.green, 2f);
                    }
                }
            }

            // 2D Shapes
            {
                ReDraw.Circle(Vector3.back * 20f, Vector3.up, DrawMode.BillboardFree, Size.Pixels(32f), Color.red);
                ReDraw.Triangle(Vector3.back * 24f, Vector3.up, DrawMode.BillboardFree, Size.Pixels(32f), Color.green);

                ReDraw.Circle(Vector3.down * 2f + Vector3.back * 20f, Vector3.up, DrawMode.BillboardAligned, Size.Pixels(32f), Color.red);
                ReDraw.Triangle(Vector3.down * 2f + Vector3.back * 24f, Vector3.up, DrawMode.BillboardAligned, Size.Pixels(32f), Color.green);

                ReDraw.Circle(Vector3.down * 4f + Vector3.back * 20f, Vector3.up, DrawMode.AxisAligned, Size.Units(2f), Color.red);
                ReDraw.Triangle(Vector3.down * 4f + Vector3.back * 24f, Vector3.up, DrawMode.AxisAligned, Size.Units(2f), Color.green);
            }

            for (int x = 0; x < 16; x++)
            {
                ReDraw.Circle2(new Vector3(-30f, Mathf.Sin(x + (float)UnityEditor.EditorApplication.timeSinceStartup * 0.1f) * 5f, 0f), Vector3.up, 3f, 36);
            }

            for (int x = 0; x < 16; x++)
            {
                ReDraw.Circle(new Vector3(-40f, Mathf.Sin(x + (float)UnityEditor.EditorApplication.timeSinceStartup * 0.1f) * 5f, 0f), Vector3.up, DrawMode.AxisAligned, Size.Units(3f), FillMode.Outline, Color.red);
            }

            // Arrows
            {
                if (arrowDirs == null || arrowDirs.Count != 64)
                {
                    arrowDirs = new List<Vector3>();
                    for (int i = 0; i < 64; i++)
                    {
                        arrowDirs.Add(Random.insideUnitSphere);
                    }
                }

                Vector3 center = Vector3.left * 18f;
                for (int i = 0; i < 64; i++)
                {
                    Vector3 arrowDir = arrowDirs[i];

                    float upDot = Mathf.Abs(Vector3.Dot(arrowDir, Vector3.up));
                    float rightDot = Mathf.Abs(Vector3.Dot(arrowDir, Vector3.right));
                    float forwardDot = Mathf.Abs(Vector3.Dot(arrowDir, Vector3.forward));
                    Color color = new Color(Mathf.Lerp(0f, 1f, rightDot), Mathf.Lerp(0f, 1f, upDot), Mathf.Lerp(0f, 1f, forwardDot), 1f);

                    ReDraw.Arrow(center, arrowDir, (ArrowCap)(i % 4), 3f, Size.Units(0.2f), 1f, color);
                }
            }
        }
    }
}