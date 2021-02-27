

using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoIconsDrawer : ReGizmoContentDrawer<ReGizmoIconDrawer>
    {
        protected override IEnumerable<ReGizmoIconDrawer> _drawers => drawers.Values;

        Dictionary<Texture2D, ReGizmoIconDrawer> drawers;

        public ReGizmoIconsDrawer() : base()
        {
            drawers = new Dictionary<Texture2D, ReGizmoIconDrawer>();
        }

        public ref IconShaderData GetShaderData(Texture2D texture)
        {
            if (!drawers.TryGetValue(texture, out var drawer))
            {
                drawer = AddSubDrawer(texture);
            }

            return ref drawer.GetShaderData();
        }

        ReGizmoIconDrawer AddSubDrawer(Texture2D texture)
        {
            var drawer = new ReGizmoIconDrawer(texture);
            drawers.Add(texture, drawer);
            return drawer;
        }
    }
}