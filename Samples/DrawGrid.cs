﻿using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class DrawGrid : MonoBehaviour
    {
        [SerializeField, Range(5, 5000)] int range = 500;
        [SerializeField] bool dyn;

        void OnDrawGizmosSelected()
        {
            ReDraw.Grid(transform.position, transform.rotation, Color.white.Darken(0.4f), range, dyn);
        }
    }
}