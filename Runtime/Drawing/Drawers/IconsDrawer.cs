

using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class IconsDrawer : ReGizmoContentDrawer<IconDrawer>
    {
        protected override IEnumerable<(IconDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Texture, (IconDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public IconsDrawer() : base()
        {
            drawers = new Dictionary<Texture, (IconDrawer, UniqueDrawData)>();
        }

        public ref IconShaderData GetShaderData(Texture texture)
        {
            if (!drawers.TryGetValue(texture, out var drawer))
            {
                drawer = AddSubDrawer(texture);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (IconDrawer, UniqueDrawData) AddSubDrawer(Texture texture)
        {
            var drawer = new IconDrawer(texture);
            drawer.SetDepthMode(depthMode);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(texture, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}