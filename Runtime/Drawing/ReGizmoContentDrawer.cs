

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal abstract class ReGizmoContentDrawer<TDrawer> : IReGizmoDrawer
        where TDrawer : IReGizmoDrawer
    {
        protected abstract IEnumerable<(TDrawer drawer, UniqueDrawData uniqueDrawData)> _drawers { get; }

        public ReGizmoContentDrawer()
        {

        }

        public void Clear()
        {
            foreach (var drawer in _drawers)
            {
                drawer.drawer.Clear();
            }
        }

        public void Dispose()
        {
            foreach (var drawer in _drawers)
            {
                drawer.drawer.Dispose();
            }
        }

        public void PushSharedData()
        {
            foreach (var drawer in _drawers)
            {
                drawer.drawer.PushSharedData();
            }
        }

        public void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            foreach (var drawer in _drawers)
            {
                drawer.drawer.Render(commandBuffer, cameraFrustum, drawer.uniqueDrawData);
            }
        }

        public uint CurrentDrawCount()
        {
            uint total = 0;
            foreach (var drawer in _drawers)
            {
                total += drawer.drawer.CurrentDrawCount();
            }
            return total;
        }
    }
}