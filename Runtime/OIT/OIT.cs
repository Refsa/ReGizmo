

using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal class OIT : System.IDisposable
    {
        RenderTexture accumulateTexture;
        RenderTexture revealageTexture;
        RenderTexture tempTargetTexture;

        public RenderTexture AccumulateTexture => accumulateTexture;
        public RenderTexture RevealageTexture => revealageTexture;

        Material blendMaterial;

        Camera camera;


        public OIT(Camera camera)
        {
            this.camera = camera;
            Resize();
            blendMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Blend");
        }

        public void Setup(CommandBuffer cmd)
        {
            cmd.SetRenderTarget(accumulateTexture);
            cmd.ClearRenderTarget(true, true, Color.clear);

            cmd.SetRenderTarget(revealageTexture);
            cmd.ClearRenderTarget(true, true, Color.white);

            if (camera.pixelHeight != accumulateTexture.height || camera.pixelWidth != accumulateTexture.width)
            {
                Resize();
            }
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer,
            CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData,
            RenderTargetIdentifier cameraTexture, RenderTargetIdentifier depthTexture)
        {
            cmd.SetRenderTarget(accumulateTexture, depthTexture);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 0);

            cmd.SetRenderTarget(revealageTexture);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 2);

            cmd.SetRenderTarget(camera.activeTexture);
        }

        public void Blend(CommandBuffer commandBuffer, RenderTargetIdentifier cameraTexture)
        {
            blendMaterial.SetTexture("_AccumTex", accumulateTexture);
            blendMaterial.SetTexture("_RevealageTex", revealageTexture);

            commandBuffer.Blit(cameraTexture, tempTargetTexture, blendMaterial);
            commandBuffer.Blit(tempTargetTexture, cameraTexture);
        }

        void Resize()
        {
            Debug.Log("OIT::Resize");

            ReleaseTexture(ref accumulateTexture);
            ReleaseTexture(ref revealageTexture);
            ReleaseTexture(ref tempTargetTexture);

            accumulateTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 0,
                RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);

            revealageTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 0,
                RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);

            tempTargetTexture = new RenderTexture(camera.pixelWidth,
                camera.pixelHeight, 24,
                RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
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