using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGizmo.Drawing;

public class DrawCubesSample : MonoBehaviour
{
    void OnDrawGizmos()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ReDraw.Cube(new Vector3(i * 2, 0, j * 2), Color.red);
            }
        }
    }
}
