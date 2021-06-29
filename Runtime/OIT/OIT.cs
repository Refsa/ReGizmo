

using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal class OIT : System.IDisposable
    {
        RenderTexture accumulateTexture;
        RenderTexture revealageTexture;

        Material accumulateMaterial;
        Material revealageMaterial;
        Material blendMaterial;

        Camera camera;


        public OIT(Camera camera)
        {
            this.camera = camera;

            accumulateTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
            revealageTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);

            accumulateMaterial = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Accumulate");
            revealageMaterial  = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Revealage");
            blendMaterial    = ReGizmoHelpers.PrepareMaterial("Hidden/OIT/Blend");
        }

        public void Render(CommandBuffer cmd, IReGizmoDrawer drawer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (camera.pixelHeight != accumulateTexture.height || camera.pixelWidth != accumulateTexture.width)
            {
                Resize();
            }

            cmd.SetRenderTarget(accumulateTexture.colorBuffer);
            cmd.ClearRenderTarget(false, true, Color.clear);
            drawer.RenderWithMaterial(cmd, cameraFrustum, uniqueDrawData, accumulateMaterial);

            cmd.SetRenderTarget(revealageTexture.colorBuffer);
            cmd.ClearRenderTarget(false, true, Color.white);
            drawer.RenderWithMaterial(cmd, cameraFrustum, uniqueDrawData, revealageMaterial);
        }

        void Resize()
        {
            accumulateTexture.Release();
            revealageTexture.Release();

            accumulateTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
            revealageTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 0, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
        }

        public void Dispose()
        {
            accumulateTexture.Release();
            revealageTexture.Release();
        }
    }
}