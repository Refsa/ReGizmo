

using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal abstract class ReGizmoContentDrawer<TDrawer> : IReGizmoDrawer
        where TDrawer : IReGizmoDrawer
    {
        protected abstract IEnumerable<TDrawer> _drawers { get; }

        public ReGizmoContentDrawer()
        {

        }

        public void Clear()
        {
            foreach (var drawer in _drawers)
            {
                drawer.Clear();
            }
        }

        public void Dispose()
        {
            foreach (var drawer in _drawers)
            {
                drawer.Dispose();
            }
        }

        public void Render(Camera camera)
        {
            foreach (var drawer in _drawers)
            {
                drawer.Render(camera);
            }
        }

        public uint CurrentDrawCount()
        {
            uint total = 0;
            foreach (var drawer in _drawers)
            {
                total += drawer.CurrentDrawCount();
            }
            return total;
        }
    }
}