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
        CullingHandler cullingHandler;
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
            cullingHandler = new CullingHandler();

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

        public void Render(List<IReGizmoDrawer> drawers)
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
                drawer.Render(commandBuffer, cullingHandler);
            }

#if REGIZMO_DEV
            commandBuffer.EndSample(profilerKey);
#endif
        }

        public void Dispose()
        {
            commandBuffer?.Release();
            cullingHandler?.Dispose();
        }
    }
}