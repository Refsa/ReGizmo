﻿using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal class DrawIcons : DrawSampleBase
    {
        [SerializeField] List<Texture2D> icons;
        [SerializeField, Range(8f, 128f)] float size = 64f;

        protected override void Draw()
        {
            if (icons == null) return;

            using (new TransformScope(transform))
            {
                int index = 0;
                foreach (var icon in icons)
                {
                    ReDraw.Icon(icon, Vector3.up * 12 * index++, Color.white, Size.Pixels(size), depthMode);
                }
            }
        }
    }
}