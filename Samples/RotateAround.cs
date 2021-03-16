using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Samples
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class RotateAround : MonoBehaviour
    {
        void Update()
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 10f * Time.deltaTime);
        }
    }
}