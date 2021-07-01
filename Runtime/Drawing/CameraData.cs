using System.Collections.Generic;
using ReGizmo.Core;
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CameraData : System.IDisposable
    {
        const int DepthTextureID = 999;

        Camera camera;
        CameraEvent cameraEvent;

        OIT oit;
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
            this.cameraEvent = cameraEvent;

            frustum = new CameraFrustum(camera);
            uniqueDrawDatas = new Dictionary<IReGizmoDrawer, UniqueDrawData>();
            oit = new OIT(camera);

            commandBuffer = new CommandBuffer();
            commandBuffer.name = $"ReGizmo Draw Buffer: {camera.name}";

            isActive = true;
            profilerKey = $"ReGizmo Camera: {camera.name}";

#if RG_LEGACY
            camera.AddCommandBuffer(cameraEvent, commandBuffer);
#endif
        }

        public void CommandBufferOverride(CommandBuffer commandBuffer)
        {
            this.commandBuffer = commandBuffer;
        }

        public void RemoveCommandBuffer()
        {
            this.commandBuffer = null;
        }

        public void DeAttach()
        {
            if (camera == null || camera.Equals(null)) return;

#if RG_LEGACY
            camera.RemoveCommandBuffer(cameraEvent, commandBuffer);
#endif
        }

        public void SetActive(bool state)
        {
            isActive = state;
        }

        public bool FrameSetup()
        {
            if (camera == null) return false;

#if !RG_HDRP
            commandBuffer.Clear();
#endif

            frustum.UpdateCameraFrustum();

            oit.Setup(commandBuffer);
            commandBuffer.SetRenderTarget(camera.activeTexture);

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

        public void PreRender(IReGizmoDrawer drawer)
        {
            if (!isActive) return;

            if (!uniqueDrawDatas.TryGetValue(drawer, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(drawer, uniqueDrawData);
            }

            drawer.PreRender(commandBuffer, frustum, uniqueDrawData);

            commandBuffer.SetRenderTarget(BuiltinRenderTextureType.Depth);
            drawer.RenderDepth(commandBuffer, frustum, uniqueDrawData);
            commandBuffer.SetRenderTarget(camera.activeTexture);
        }

        public void Render(IReGizmoDrawer drawer)
        {
            if (!isActive) return;

            if (!uniqueDrawDatas.TryGetValue(drawer, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(drawer, uniqueDrawData);
            }

            // drawer.Render(commandBuffer, frustum, uniqueDrawData);
            oit.Render(commandBuffer, drawer, frustum, uniqueDrawData, camera.activeTexture, BuiltinRenderTextureType.Depth);
        }

        public void PostRender()
        {
            oit.Blend(commandBuffer, camera.activeTexture);
            // commandBuffer.Blit(BuiltinRenderTextureType.Depth, camera.activeTexture);
            // commandBuffer.Blit(oit.AccumulateTexture, camera.activeTexture);
            // commandBuffer.Blit(oit.RevealageTexture, camera.activeTexture);
            // commandBuffer.ReleaseTemporaryRT(DepthTextureID);

#if REGIZMO_DEV
            commandBuffer.EndSample(profilerKey);
#endif
        }

        public void Dispose()
        {
            foreach (var data in uniqueDrawDatas.Values)
            {
                data?.Dispose();
            }
            oit?.Dispose();
        }
    }
}