

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

            accumulateTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, 
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);

            revealageTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, 
                RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
                
            tempTargetTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 32, 
                RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);

            blendMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Blend");
        }

        public void Setup(CommandBuffer cmd)
        {
            cmd.SetRenderTarget(accumulateTexture);
            cmd.ClearRenderTarget(true, true, Color.clear);

            cmd.SetRenderTarget(revealageTexture);
            cmd.ClearRenderTarget(true, true, Color.white);
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer, 
            CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, 
            RenderTargetIdentifier cameraTexture, RenderTargetIdentifier depthTexture)
        {
            if (camera.pixelHeight != accumulateTexture.height || camera.pixelWidth != accumulateTexture.width)
            {
                Resize();
            }

            cmd.SetRenderTarget(accumulateTexture, depthTexture);
            drawer.RenderWithPass(cmd, cameraFrustum, uniqueDrawData, 0);

            cmd.SetRenderTarget(revealageTexture, depthTexture);
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

            accumulateTexture.Release();
            revealageTexture.Release();
            tempTargetTexture.Release();

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
            accumulateTexture.Release();
            revealageTexture.Release();
            tempTargetTexture.Release();
        }
    }
}