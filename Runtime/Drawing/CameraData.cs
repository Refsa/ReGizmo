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
        Dictionary<IReGizmoDrawer, UniqueDrawData> uniqueDrawDatas;

        bool isActive;

        string profilerKey;

        public string ProfilerKey => profilerKey;
        public CommandBuffer CommandBuffer => commandBuffer;
        public Camera Camera => camera;

        public CameraData(Camera camera, CameraEvent cameraEvent)
        {
            this.camera = camera;
            frustum = new CameraFrustum(camera);
            uniqueDrawDatas = new Dictionary<IReGizmoDrawer, UniqueDrawData>();

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

        public void PreRender()
        {
            commandBuffer.Clear();
            frustum.UpdateCameraFrustum();

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
        }

        public void PostRender()
        {
#if REGIZMO_DEV
            commandBuffer.EndSample(profilerKey);
#endif
        }

        public void Render(IReGizmoDrawer drawer)
        {
            if (!isActive) return;

            if (!uniqueDrawDatas.TryGetValue(drawer, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(drawer, uniqueDrawData);
            }

            drawer.Render(commandBuffer, frustum, uniqueDrawData);
        }

        public void Dispose()
        {
            commandBuffer?.Release();

            foreach (var data in uniqueDrawDatas.Values)
            {
                data?.Dispose();
            }
        }
    }
}