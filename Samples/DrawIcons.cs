using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class DrawIcons : MonoBehaviour
    {
        [SerializeField] List<Texture2D> icons;

        void OnDrawGizmos()
        {
            if (icons == null) return;

            using (new TransformScope(transform))
            {
                int index = 0;
                foreach (var icon in icons)
                {
                    ReDraw.Icon(icon, Vector3.up * 12 * index++, Color.black, 32f);
                }
            }
        }
    }
}