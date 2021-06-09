using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal class DrawSprites : MonoBehaviour
    {
        [SerializeField] List<Sprite> sprites;

        void OnDrawGizmos()
        {
            if (sprites == null) return;

            using (new TransformScope(transform))
            {
                int index = 0;
                foreach (var sprite in sprites)
                {
                    ReDraw.Sprite(sprite, Vector3.up * 12 * index++, 4f);
                }
            }
        }
    }
}