﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Samples
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    internal class RotateAround : MonoBehaviour
    {
        [SerializeField] Transform pivot;
        [SerializeField] float speed = 10f;

        void Update()
        {
            transform.RotateAround(pivot == null ? Vector3.zero : pivot.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}