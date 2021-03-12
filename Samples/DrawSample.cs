using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;

public class DrawSample : MonoBehaviour
{
    [SerializeField] Mesh[] customMeshes;
    [SerializeField] Texture2D[] customIcons;

    Color[] colors = new Color[] { Color.red, Color.green, Color.blue };

    void OnDrawGizmos()
    {
        Color cubeColor = Color.black;
        cubeColor.a = 1f;
        for (int i = 0; i < 16; i++)
        {
            cubeColor.r = (float)i / 16f;
            for (int j = 0; j < 16; j++)
            {
                cubeColor.g = (float)j / 16f;
                ReDraw.Cube(new Vector3(10 + i * 2, 0, 10 + j * 2), Quaternion.identity, Vector3.one, cubeColor);
            }
        }

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
            int index = 0;
            foreach (var cm in customMeshes)
            {
                ReDraw.Mesh(cm, Vector3.up * index * 5, Quaternion.identity, Vector3.one, colors[index % 3]);
                index++;
            }
        }

        // Text
        {
            string text = "Hello From ReGizmo";

            ReDraw.Text(text, Vector3.back * 5, 1f, Color.green);
            ReDraw.Text(text, Vector3.back * 7, 1f, Color.green);
        }

        // Lines
        {
            ReDraw.Line(Vector3.left, Vector3.left * 10, Color.cyan, Color.green, 5f, 5f);
            ReDraw.Line(Vector3.up + Vector3.left, Vector3.up + Vector3.left * 10, Color.cyan, Color.green, 5f, 5f);
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
    }
}
