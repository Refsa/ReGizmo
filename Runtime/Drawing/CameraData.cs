using System.Collections.Generic;
using ReGizmo.Core;
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CameraData : System.IDisposable
    {
        Camera camera;
        CameraFrustum frustum;
        CommandBuffer commandBuffer;
        bool isActive;

        string profilerKey;

        public string ProfilerKey => profilerKey;
        public CommandBuffer CommandBuffer => commandBuffer;
        public Camera Camera => camera;

        public CameraData(Camera camera, CameraEvent cameraEvent)
        {
            this.camera = camera;
            frustum = new CameraFrustum(camera);

            commandBuffer = new CommandBuffer();
            commandBuffer.name = $"ReGizmo Draw Buffer: {camera.name}";

            isActive = true;
            profilerKey = $"ReGizmo Camera: {camera.name}";

            camera.AddCommandBuffer(cameraEvent, commandBuffer);
        }

        public void SetActive(bool state)
        {
            isActive = state;
        }

        public void Render(in List<IReGizmoDrawer> drawers)
        {
            commandBuffer.Clear();
            if (!isActive) return;

            var frustumPlanes = frustum.UpdateCameraFrustum();
            commandBuffer.SetComputeVectorArrayParam(CullingHandler.CullingCompute, "_CameraFrustum", frustumPlanes);
            commandBuffer.SetComputeVectorParam(CullingHandler.CullingCompute, "_CameraClips", new Vector2(camera.nearClipPlane, camera.farClipPlane));

#if REGIZMO_DEV
            commandBuffer.BeginSample(profilerKey);
#endif

            if (ReGizmoSettings.FontSuperSample)
            {
                commandBuffer.EnableShaderKeyword(ReGizmoHelpers.ShaderFontSuperSamplingKeyword);
            }
            else
            {
                commandBuffer.DisableShaderKeyword(ReGizmoHelpers.ShaderFontSuperSamplingKeyword);
            }

            foreach (var drawer in drawers)
            {
                if (drawer.CurrentDrawCount() == 0) continue;
                drawer.Render(commandBuffer);
            }

#if REGIZMO_DEV
            commandBuffer.EndSample(profilerKey);
#endif
        }

        public void Dispose()
        {
            commandBuffer?.Release();
        }
    }
}