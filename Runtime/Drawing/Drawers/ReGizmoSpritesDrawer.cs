using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoSpritesDrawer : ReGizmoContentDrawer<ReGizmoSpriteDrawer>
    {
        protected override IEnumerable<(ReGizmoSpriteDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Sprite, (ReGizmoSpriteDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public ReGizmoSpritesDrawer() : base()
        {
            drawers = new Dictionary<Sprite, (ReGizmoSpriteDrawer, UniqueDrawData)>();
        }

        public ref SpriteShaderData GetShaderData(Sprite sprite)
        {
            if (!drawers.TryGetValue(sprite, out var drawer))
            {
                drawer = AddSubDrawer(sprite);
            }

            ref var data = ref drawer.drawer.GetShaderData(); ;

            data.UVs = drawer.drawer.SpriteUVs;

            return ref data;
        }

        (ReGizmoSpriteDrawer, UniqueDrawData) AddSubDrawer(Sprite sprite)
        {
            var drawer = new ReGizmoSpriteDrawer(sprite);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(sprite, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}