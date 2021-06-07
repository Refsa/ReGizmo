using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class SpritesDrawer : ReGizmoContentDrawer<SpriteDrawer>
    {
        protected override IEnumerable<(SpriteDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Sprite, (SpriteDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public SpritesDrawer() : base()
        {
            drawers = new Dictionary<Sprite, (SpriteDrawer, UniqueDrawData)>();
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

        (SpriteDrawer, UniqueDrawData) AddSubDrawer(Sprite sprite)
        {
            var drawer = new SpriteDrawer(sprite);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(sprite, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}