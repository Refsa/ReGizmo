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
        CameraEvent cameraEvent;

        CameraFrustum frustum;
        CommandBuffer commandBuffer;
        Dictionary<IReGizmoDrawer, UniqueDrawData> uniqueDrawDatas;

        bool isActive;

        string profilerKey;

        public string ProfilerKey => profilerKey;
        public CommandBuffer CommandBuffer => commandBuffer;
        public Camera Camera => camera;

        public static CameraData Legacy(Camera camera, CameraEvent cameraEvent)
        {
            var cameraData = new CameraData();

            cameraData.camera = camera;
            cameraData.frustum = new CameraFrustum(camera);
            cameraData.uniqueDrawDatas = new Dictionary<IReGizmoDrawer, UniqueDrawData>();

            cameraData.commandBuffer = new CommandBuffer();
            cameraData.commandBuffer.name = $"ReGizmo Draw Buffer: {camera.name}";

            cameraData.isActive = true;
            cameraData.profilerKey = $"ReGizmo Camera: {camera.name}";

            cameraData.cameraEvent = cameraEvent;
            camera.AddCommandBuffer(cameraEvent, cameraData.commandBuffer);

            return cameraData;
        }

        public static CameraData SRP(Camera camera)
        {
            var cameraData = new CameraData();

            cameraData.camera = camera;
            cameraData.frustum = new CameraFrustum(camera);
            cameraData.uniqueDrawDatas = new Dictionary<IReGizmoDrawer, UniqueDrawData>();

            cameraData.commandBuffer = new CommandBuffer();
            cameraData.commandBuffer.name = $"ReGizmo Draw Buffer: {camera.name}";

            cameraData.isActive = true;
            cameraData.profilerKey = $"ReGizmo Camera: {camera.name}";

            return cameraData;
        }

        public void DeAttach()
        {
            if (camera == null || camera.Equals(null)) return;

            camera.RemoveCommandBuffer(cameraEvent, commandBuffer);
        }

        public void SetActive(bool state)
        {
            isActive = state;
        }

        public bool PreRender()
        {
            if (camera == null) return false;

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

            return true;
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