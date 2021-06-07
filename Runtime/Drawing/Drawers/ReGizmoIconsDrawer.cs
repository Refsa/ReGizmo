

using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoIconsDrawer : ReGizmoContentDrawer<IconDrawer>
    {
        protected override IEnumerable<(IconDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Texture2D, (IconDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public ReGizmoIconsDrawer() : base()
        {
            drawers = new Dictionary<Texture2D, (IconDrawer, UniqueDrawData)>();
        }

        public ref IconShaderData GetShaderData(Texture2D texture)
        {
            if (!drawers.TryGetValue(texture, out var drawer))
            {
                drawer = AddSubDrawer(texture);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (IconDrawer, UniqueDrawData) AddSubDrawer(Texture2D texture)
        {
            var drawer = new IconDrawer(texture);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(texture, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}