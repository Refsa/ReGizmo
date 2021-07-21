#if RG_HDRP
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace ReGizmo.HDRP
{
    class OITHDRP : IOIT
    {
        Material blendMaterial;
        Material blitMaterial;

        RenderTexture revealageTexture;
        RTHandle accumulateTexture;
        RTHandle tempTargetTexture;

        Camera camera;

        public OITHDRP(Camera camera)
        {
            this.camera = camera;
        }

        public void Setup(CommandBuffer commandBuffer, in Framebuffer framebuffer)
        {
            blendMaterial = CoreUtils.CreateEngineMaterial("Hidden/OIT/Blend");
            blitMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/CopyColor");

            revealageTexture = RTHandles.Alloc(framebuffer.ColorTarget.referenceSize.x, framebuffer.ColorTarget.referenceSize.y,
                colorFormat: GraphicsFormat.R16_SFloat);

            accumulateTexture = RTHandles.Alloc(framebuffer.ColorTarget.referenceSize.x, framebuffer.ColorTarget.referenceSize.y,
                colorFormat: GraphicsFormat.R16G16B16A16_SFloat);
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, in Framebuffer framebuffer)
        {
            CoreUtils.SetRenderTarget(cmd, accumulateTexture, ClearFlag.Color, Color.clear);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 0);

            CoreUtils.SetRenderTarget(cmd, revealageTexture, framebuffer.DepthTarget, ClearFlag.Color, Color.white);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 2);
        }

        public void Blend(CommandBuffer commandBuffer, in Framebuffer framebuffer)
        {
            commandBuffer.SetGlobalTexture("_AccumTex", accumulateTexture);
            commandBuffer.SetGlobalTexture("_RevealageTex", revealageTexture);

            commandBuffer.Blit(framebuffer.ColorTarget, framebuffer.ColorTarget, blendMaterial);
        }

        public void FrameCleanup()
        {
            CoreUtils.Destroy(blendMaterial);
            CoreUtils.Destroy(blitMaterial);

            RTHandles.Release(accumulateTexture);
            RTHandles.Release(revealageTexture);
        }

        public void Dispose()
        {

        }
    }
}
#endif