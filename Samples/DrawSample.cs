using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;
using UnityEditor;

namespace ReGizmo.Samples
{
    [AddComponentMenu("ReGizmo Samples/Draw Sample")]
    public class DrawSample : MonoBehaviour
    {
        [SerializeField] Mesh someMesh;

        [SerializeField] Mesh[] customMeshes;
        [SerializeField] Texture2D[] customIcons;

        Color[] colors = new Color[] {Color.red, Color.green, Color.blue};

        Vector3[] someMeshVertices;
        int[] someMeshIndices;

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
                time = (float) EditorApplication.timeSinceStartup;
            }
#endif

            Color cubeColor = Color.black;
            cubeColor.a = 1f;
            for (int i = 0; i < 16; i++)
            {
                cubeColor.r = (float) i / 16f;
                for (int j = 0; j < 16; j++)
                {
                    cubeColor.g = (float) j / 16f;
                    ReDraw.Cube(new Vector3(10 + i * 2, 0f, 10 + j * 2),
                        Quaternion.Euler(
                            (i * Mathf.Sin(time) * 5.625f),
                            0f,
                            (j * Mathf.Sin(time) * 5.625f)),
                        Vector3.one, cubeColor);
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

                ReDraw.Text(text, Vector3.back * 5, 1f, Color.green);
                ReDraw.TextSDF(text, Vector3.back * 7, 1f, Color.green);

                text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890!#{}[]()";

                ReDraw.TextSDF("SDF: " + text, Vector3.up * 10f + Vector3.right * 15f, 2f, Color.blue);
                ReDraw.Text("REG: " + text, Vector3.up * 8f + Vector3.right * 15f, 2f, Color.blue);
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
                ReDraw.TextSDF("Raycast", Vector3.up * 21.1f, 1f, Color.blue);
            }

            // Spherecast
            {
                ReDraw.SphereCast(Vector3.up * 20f + Vector3.forward * 5f, Vector3.right, 0.5f, 100f);
                ReDraw.SphereCast(Vector3.up * 21f + Vector3.forward * 5f, Vector3.right, 0.5f, 100f);
                ReDraw.SphereCast(Vector3.up * 22f + Vector3.forward * 5f, Vector3.right, 0.5f, 100f);
                ReDraw.TextSDF("Spherecast", Vector3.up * 22.1f + Vector3.forward * 5f, 1f, Color.blue);
            }

            // Boxcast
            {
                ReDraw.BoxCast(Vector3.up * 20f + Vector3.forward * 10f, Vector3.right, Vector3.one * 0.5f,
                    Quaternion.identity, 100f);
                ReDraw.BoxCast(Vector3.up * 21f + Vector3.forward * 10f, Vector3.right, Vector3.one * 0.5f,
                    Quaternion.Euler(45f, 45f, 0f), 100f);
                ReDraw.BoxCast(Vector3.up * 22f + Vector3.forward * 10f, Vector3.right, Vector3.one * 0.5f,
                    Quaternion.Euler(0f, 45f, 45f), 100f);
                ReDraw.TextSDF("Boxcast", Vector3.up * 22.1f + Vector3.forward * 10f, 1f, Color.blue);
            }

            // Custom Icons
            {
                for (int i = 0; i < customIcons.Length; i++)
                {
                    ReDraw.Icon(customIcons[i], Vector3.left * 5 + Vector3.up * 2 * i, Color.black, 1f);
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
        }
    }
}