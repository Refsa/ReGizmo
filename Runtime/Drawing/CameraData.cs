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

        IOIT oit;
        CameraFrustum frustum;
        CommandBuffer commandBuffer;
        Framebuffer framebuffer;
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

#if RG_HDRP
            oit = new ReGizmo.HDRP.OITHDRP(camera);
#else
            oit = new OIT(camera);
#endif

            commandBuffer = new CommandBuffer();
            commandBuffer.name = $"ReGizmo Draw Buffer: {camera.name}";

            isActive = true;
            profilerKey = $"ReGizmo Camera: {camera.name}";

#if RG_LEGACY
            camera.depthTextureMode |= DepthTextureMode.Depth;
            Attach();
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

        public void Attach()
        {
            if (camera == null || camera.Equals(null)) return;

#if RG_LEGACY
            camera.AddCommandBuffer(cameraEvent, commandBuffer);
#endif
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
            if (!isActive)
            {
                DeAttach();
            }
            else
            {
                Attach();
            }
        }

        public void SetFramebuffer(in Framebuffer framebuffer)
        {
            this.framebuffer = framebuffer;
        }

        public bool FrameSetup(bool clearCommandBuffer = true)
        {
            if (camera == null) return false;

            if (clearCommandBuffer)
            {
                commandBuffer.Clear();
            }

#if RG_LEGACY
            framebuffer = new Framebuffer
            {
                ColorTarget = BuiltinRenderTextureType.CameraTarget,
                DepthTarget = BuiltinRenderTextureType.CameraTarget,
            };

            commandBuffer.SetRenderTarget(framebuffer.ColorTarget);
            commandBuffer.ClearRenderTarget(true, false, Color.clear);
            ShaderUtils.CopyDepth(commandBuffer, framebuffer.ColorTarget, BuiltinRenderTextureType.Depth);
#endif

            frustum.UpdateCameraFrustum();
            // oit.Setup(commandBuffer, framebuffer);
            commandBuffer.SetGlobalFloat("_AlphaBehindScale", ReGizmoSettings.AlphaBehindScale);

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

        public void PreRender(List<IReGizmoDrawer> drawers)
        {
            if (!isActive) return;

            commandBuffer.SetRenderTarget(framebuffer.ColorTarget, framebuffer.DepthTarget);

            foreach (var drawer in drawers)
            {
                PreRender(drawer);
            }
        }

        void PreRender(IReGizmoDrawer drawer)
        {
            if (!isActive) return;

            if (!uniqueDrawDatas.TryGetValue(drawer, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(drawer, uniqueDrawData);
            }

            drawer.PreRender(commandBuffer, frustum, uniqueDrawData);
            drawer.RenderDepth(commandBuffer, frustum, uniqueDrawData);
        }

        public void Render(List<IReGizmoDrawer> drawers)
        {
            commandBuffer.SetRenderTarget(framebuffer.ColorTarget, framebuffer.DepthTarget);

            foreach (var drawer in drawers)
            {
                Render(drawer);
            }
        }

        void Render(IReGizmoDrawer drawer)
        {
            if (!uniqueDrawDatas.TryGetValue(drawer, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(drawer, uniqueDrawData);
            }

            drawer.RenderWithPass(commandBuffer, frustum, uniqueDrawData, 3);
            drawer.RenderWithPass(commandBuffer, frustum, uniqueDrawData, 4);

            // oit.Render(commandBuffer, drawer, frustum, uniqueDrawData, framebuffer);
        }

        public void PostRender()
        {
            // oit.Blend(commandBuffer, framebuffer);

#if REGIZMO_DEV
            commandBuffer.EndSample(profilerKey);
#endif
        }

        public void FrameCleanup()
        {
            // oit.FrameCleanup();

#if RG_HDRP
            framebuffer = new Framebuffer();
#endif
        }

        public void Dispose()
        {
            foreach (var data in uniqueDrawDatas.Values)
            {
                data?.Dispose();
            }
            // oit?.Dispose();
        }
    }
}

