using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;

public class DrawSample : MonoBehaviour
{
    [SerializeField] Mesh[] customMeshes;
    [SerializeField] Texture2D[] customIcons;

    void OnDrawGizmos()
    {
        using (new ColorScope(Color.green))
        {
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    ReDraw.Cube(new Vector3(10 + i * 2, 0, 10 + j * 2), Quaternion.identity, Vector3.one);
                }
            }
        }

        if (customMeshes != null)
        {
            int index = 0;
            foreach (var cm in customMeshes)
            {
                ReDraw.Mesh(cm, Vector3.up * index * 5, Quaternion.identity, Vector3.one, Color.blue);
                index++;
            }
        }

        // Text
        {
            string text = "Hello From ReGizmo";

            ReDraw.Text(text, Vector3.back * 5, 1f, Color.green);
        }

        // Lines
        {
            ReDraw.Line(Vector3.left, Vector3.left * 10, Color.cyan, Color.green, 5f, 10f);
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
            }
        }
    }
}
