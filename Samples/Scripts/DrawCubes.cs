using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class DrawCubes : MonoBehaviour
{
    [SerializeField] int sizeSqr = 64;

#if UNITY_EDITOR
    void OnDrawGizmos()
#else
    void Update()
#endif
    {
        using (new TransformScope(transform))
        {
            for (int x = 0; x < sizeSqr; x++)
                for (int y = 0; y < sizeSqr; y++)
                {
                    float scalar = (0.01f + (x / (float)sizeSqr)) * 100;
                    var scale = new Vector3(scalar, scalar, scalar);

                    ReDraw.Cube(new Vector3(x * scalar * 2, 0, y * scalar * 2), scale, Color.red);
                }
        }
    }
}
