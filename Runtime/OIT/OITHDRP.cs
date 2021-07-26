#if RG_HDRP
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ReGizmo.HDRP
{
    class OITHDRP : IOIT
    {
        Material blendMaterial;
        Material blitMaterial;
        Material clearMaterial;

        RTHandle revealageTexture;
        RTHandle accumulateTexture;
        RTHandle tempTargetTexture;

        Camera camera;

        public OITHDRP(Camera camera)
        {
            this.camera = camera;

            blendMaterial = CoreUtils.CreateEngineMaterial("Hidden/OIT/HDRP_Blend");
            blitMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/CopyFramebuffer");
            clearMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/HDRP_Clear");

            tempTargetTexture = RTHandles.Alloc(Vector2.one,
                useDynamicScale: true,
                dimension: TextureXR.dimension,
                colorFormat: GraphicsFormat.R8G8B8A8_SRGB,
                name: "TempColorTarget");

            revealageTexture = RTHandles.Alloc(Vector2.one,
                useDynamicScale: true,
                dimension: TextureXR.dimension,
                colorFormat: GraphicsFormat.R16_SFloat,
                name: "OIT_Revealage");

            accumulateTexture = RTHandles.Alloc(Vector2.one,
                useDynamicScale: true,
                dimension: TextureXR.dimension,
                colorFormat: GraphicsFormat.R16G16B16A16_SFloat,
                name: "OIT_Accumulate");
        }

        public void Setup(CommandBuffer commandBuffer, in Framebuffer framebuffer)
        {
            commandBuffer.SetGlobalTexture("_InputTexture", framebuffer.ColorTarget);
            commandBuffer.Blit(framebuffer.ColorTarget, tempTargetTexture, blitMaterial, 0);

            commandBuffer.SetGlobalColor("_ClearColor", Color.clear);
            commandBuffer.Blit(null, accumulateTexture, clearMaterial);

            commandBuffer.SetGlobalColor("_ClearColor", Color.white);
            commandBuffer.Blit(null, revealageTexture, clearMaterial);
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, in Framebuffer framebuffer)
        {
            cmd.SetRenderTarget(accumulateTexture, framebuffer.DepthTarget);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 0);

            cmd.SetRenderTarget(revealageTexture);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 2);
        }

        public void Blend(CommandBuffer commandBuffer, in Framebuffer framebuffer)
        {
            commandBuffer.SetGlobalTexture("_AccumTex", accumulateTexture);
            commandBuffer.SetGlobalTexture("_RevealageTex", revealageTexture);
            commandBuffer.SetGlobalTexture("_BackgroundTex", tempTargetTexture);
            commandBuffer.Blit(tempTargetTexture, framebuffer.ColorTarget, blendMaterial);
        }

        public void FrameCleanup()
        {
            
        }

        public void Dispose()
        {
            CoreUtils.Destroy(blendMaterial);
            CoreUtils.Destroy(blitMaterial);
            CoreUtils.Destroy(clearMaterial);

            RTHandles.Release(tempTargetTexture);
            RTHandles.Release(accumulateTexture);
            RTHandles.Release(revealageTexture);
        }
    }
}
#endif