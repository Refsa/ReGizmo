

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

        public void PushSharedData()
        {
            foreach (var drawer in _drawers)
            {
                drawer.PushSharedData();
            }
        }

        public void Render(CommandBuffer commandBuffer, CullingHandler cullingHandler, UniqueDrawData uniqueDrawData)
        {
            foreach (var drawer in _drawers)
            {
                drawer.Render(commandBuffer, cullingHandler, uniqueDrawData);
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