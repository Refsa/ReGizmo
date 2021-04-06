﻿using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoSpritesDrawer : ReGizmoContentDrawer<ReGizmoSpriteDrawer>
    {
        protected override IEnumerable<ReGizmoSpriteDrawer> _drawers => drawers.Values;

        Dictionary<Sprite, ReGizmoSpriteDrawer> drawers;

        public ReGizmoSpritesDrawer() : base()
        {
            drawers = new Dictionary<Sprite, ReGizmoSpriteDrawer>();
        }

        public ref SpriteShaderData GetShaderData(Sprite sprite)
        {
            if (!drawers.TryGetValue(sprite, out var drawer))
            {
                drawer = AddSubDrawer(sprite);
            }

            return ref drawer.GetShaderData();
        }

        ReGizmoSpriteDrawer AddSubDrawer(Sprite sprite)
        {
            var drawer = new ReGizmoSpriteDrawer(sprite);
            drawers.Add(sprite, drawer);
            return drawer;
        }
    }
}