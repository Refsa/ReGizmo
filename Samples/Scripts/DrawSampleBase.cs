using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal abstract class DrawSampleBase : MonoBehaviour
    {
        [SerializeField] protected DepthMode depthMode = DepthMode.Sorted;

        protected abstract void Draw();

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Draw();
        }
#else
        void Update()
        {
            Draw();
        }
#endif
    }
}