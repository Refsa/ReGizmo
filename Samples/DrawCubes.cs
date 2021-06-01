using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class DrawCubes : MonoBehaviour
{
    [SerializeField] int sizeSqr = 64;

    void OnDrawGizmos()
    {
        using (new TransformScope(transform))
        {
            for (int x = 0; x < sizeSqr; x++)
                for (int y = 0; y < sizeSqr; y++)
                {
                    ReDraw.Cube(new Vector3(x * 2, 0, y * 2), Color.red);
                }
        }
    }
}
