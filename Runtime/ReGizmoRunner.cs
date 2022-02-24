

using System;
using System.Collections.Generic;
using ReGizmo.Core;
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Core
{
    internal abstract class ReGizmoRunner : System.IDisposable
    {
        protected List<IReGizmoDrawer> drawers;
        protected Dictionary<Camera, CameraData> activeCameras;

        public virtual void Init(List<IReGizmoDrawer> drawers)
        {
            this.drawers = drawers;
        }

        public abstract void Execute();
        public abstract void Cleanup();

        public virtual void Dispose()
        {
            if (activeCameras != null)
            {
                foreach (var cameraData in activeCameras.Values)
                {
                    cameraData.DeAttach();
                    cameraData.Dispose();
                }

                activeCameras.Clear();
                activeCameras = null;
            }

            if (drawers != null)
            {
                foreach (var drawer in drawers)
                {
                    drawer.Dispose();
                }

                drawers.Clear();
                drawers = null;
            }
        }

        protected void Render(CameraData cameraData, bool clearCommandBuffer = true)
        {
            if (!cameraData.FrameSetup(clearCommandBuffer))
            {
                return;
            }

            cameraData.PreRender(drawers);
            cameraData.Render(drawers);
            cameraData.PostRender();
        }
    }
}

#if RG_HDRP
namespace ReGizmo.Core.HDRP
{
    internal class ReGizmoHDRPRunner : ReGizmoRunner
    {
        public override void Init(List<IReGizmoDrawer> drawers)
        {
            base.Init(drawers);

            Core.HDRP.ReGizmoHDRPRenderPass.OnPassExecute += OnPassExecute;
            Core.HDRP.ReGizmoHDRPRenderPass.OnPassCleanup += OnPassCleanup;
        }

        public override void Cleanup()
        {

        }

        public override void Execute()
        {

        }

        private void OnPassCleanup()
        {

        }

        private void OnPassExecute(in ScriptableRenderContext context, CommandBuffer cmd, Camera camera, in Framebuffer framebuffer)
        {

        }

        public override void Dispose()
        {
            base.Dispose();

            Core.HDRP.ReGizmoHDRPRenderPass.OnPassExecute -= OnPassExecute;
            Core.HDRP.ReGizmoHDRPRenderPass.OnPassCleanup -= OnPassCleanup;
        }
    }
}
#endif