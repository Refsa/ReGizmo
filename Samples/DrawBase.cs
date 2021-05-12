using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal abstract class DrawBase : MonoBehaviour
    {
        void Update()
        {
            Draw();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            DrawGizmos();
        }
#endif

        protected abstract void Draw();
        protected abstract void DrawGizmos();
    }
}