

using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal interface IOIT : System.IDisposable
    {
        void Setup(CommandBuffer commandBuffer, in Framebuffer framebuffer);
        void Render(CommandBuffer cmd, IReGizmoDrawer drawer,
            CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData,
            in Framebuffer framebuffer);
        void Blend(CommandBuffer commandBuffer, in Framebuffer framebuffer);
        void FrameCleanup();
    }

    internal class OIT : IOIT
    {
        RenderTexture accumulateTexture;
        RenderTexture revealageTexture;
        RenderTexture tempTargetTexture;

        public RenderTexture AccumulateTexture => accumulateTexture;
        public RenderTexture RevealageTexture => revealageTexture;

        Material blendMaterial;
        Material blitMaterial;

        Camera camera;

        public OIT(Camera camera)
        {
            this.camera = camera;
            Resize();

            blendMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Blend");
            // blitMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/BlitCopy");
            blitMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/CopyColor");
        }

        public void Setup(CommandBuffer cmd, in Framebuffer framebuffer)
        {
            if (camera.pixelHeight != accumulateTexture.height || camera.pixelWidth != accumulateTexture.width)
            {
                Resize();
            }

            cmd.Blit(framebuffer.ColorTarget, tempTargetTexture, blitMaterial);

            cmd.SetRenderTarget(accumulateTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
            cmd.ClearRenderTarget(false, true, Color.clear);

            cmd.SetRenderTarget(revealageTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
            cmd.ClearRenderTarget(false, true, Color.white);
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer,
            CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData,
            in Framebuffer framebuffer)
        {
            // cmd.SetRenderTarget(tempTargetTexture, framebuffer.DepthTarget);
            // drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 4); 

            cmd.SetRenderTarget(accumulateTexture, framebuffer.DepthTarget);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 0);

            cmd.SetRenderTarget(revealageTexture);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 2);
        }

        public void Blend(CommandBuffer commandBuffer, in Framebuffer framebuffer)
        {
            blendMaterial.SetTexture("_AccumTex", accumulateTexture);
            blendMaterial.SetTexture("_RevealageTex", revealageTexture);

            commandBuffer.Blit(tempTargetTexture, framebuffer.ColorTarget, blendMaterial);
        }

        public void FrameCleanup() { }

        protected void Resize()
        {
            Debug.Log("OIT::Resize");

            ReleaseTexture(ref accumulateTexture);
            ReleaseTexture(ref revealageTexture);
            ReleaseTexture(ref tempTargetTexture);

            accumulateTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 0,
                RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
            accumulateTexture.name = "AccumulateTexture";

            revealageTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 0,
                RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
            revealageTexture.name = "RevealageTexture";

            tempTargetTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 24,
                RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
            tempTargetTexture.name = "TempTargetTexture";
        }

        public void Dispose()
        {
            ReleaseTexture(ref accumulateTexture);
            ReleaseTexture(ref revealageTexture);
            ReleaseTexture(ref tempTargetTexture);
        }

        static void ReleaseTexture(ref RenderTexture texture)
        {
            if (texture != null && !texture.Equals(null) && texture.IsCreated())
            {
                texture.Release();
            }
            texture = null;
        }
    }
}