

using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoIconsDrawer : ReGizmoContentDrawer<ReGizmoIconDrawer>
    {
        protected override IEnumerable<(ReGizmoIconDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Texture2D, (ReGizmoIconDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public ReGizmoIconsDrawer() : base()
        {
            drawers = new Dictionary<Texture2D, (ReGizmoIconDrawer, UniqueDrawData)>();
        }

        public ref IconShaderData GetShaderData(Texture2D texture)
        {
            if (!drawers.TryGetValue(texture, out var drawer))
            {
                drawer = AddSubDrawer(texture);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (ReGizmoIconDrawer, UniqueDrawData) AddSubDrawer(Texture2D texture)
        {
            var drawer = new ReGizmoIconDrawer(texture);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(texture, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}