using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine;

public class DrawSDFText : MonoBehaviour
{
    [SerializeField] string text = "Hello";
    [SerializeField, Range(1f, 32f)] float fontSize = 1f;

    void OnDrawGizmos()
    {
        ReDraw.TextSDF(text, Vector3.left * 0.15f, fontSize, Color.green);
        ReDraw.Text(text, Vector3.right * 0.15f, fontSize, Color.green);

        Handles.Label(Vector3.left * 0.15f, text);
        Handles.Label(Vector3.right * 0.15f, text);
    }
}