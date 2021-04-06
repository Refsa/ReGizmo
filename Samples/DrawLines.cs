using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [SerializeField, Range(1f, 32f)] private float width = 1f;
    
    private void OnDrawGizmos()
    {
        using (new TransformScope(transform))
        {
            for (int i = 0; i < 32; i++)
            {
                ReDraw.Ray(new Vector3(0, 0, i), Vector3.right * (i + 1), Color.red, width);
            }
        }
    }
}
