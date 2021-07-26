#if RG_HDRP
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace ReGizmo.Core.HDRP
{
    class ReGizmoHDRPRenderPass : CustomPass
    {
        public delegate void PassExecuteDelegate(in ScriptableRenderContext context, CommandBuffer cmd, Camera camera, in Framebuffer framebuffer);

        public static event PassExecuteDelegate OnPassExecute;
        public static event System.Action OnPassCleanup;

        Material copyFramebufferMaterial;
        Material clearMaterial;
        Material copyDepthMaterial;

        RTHandle depthBuffer;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            name = "ReGizmoHDRPPass";

            clearMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/HDRP_Clear");
            copyFramebufferMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/CopyFramebuffer");
            copyDepthMaterial = CoreUtils.CreateEngineMaterial("Hidden/HDRP/CopyDepthBuffer");

            depthBuffer = RTHandles.Alloc(Vector2.one,
                useDynamicScale: true,
                dimension: TextureXR.dimension,
                colorFormat: GraphicsFormat.R8G8B8A8_SNorm | GraphicsFormat.R8G8_SNorm | GraphicsFormat.RG_EAC_SNorm,
                name: "TempColorTarget",
                depthBufferBits: DepthBits.Depth24);
        }

        protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
        {
            SetCameraRenderTarget(cmd);
            GetCameraBuffers(out var color, out var depth);
            var framebuffer = new Framebuffer(color, depthBuffer);

            // cmd.SetGlobalColor("_ClearColor", Color.clear);
            // cmd.SetGlobalFloat("_ClearDepth", 0f);
            // cmd.Blit(null, framebuffer.DepthTarget, clearMaterial, 1);

            CoreUtils.SetRenderTarget(cmd, framebuffer.DepthTarget, ClearFlag.All);

            Vector4 scaleFactor = new Vector4(
                depth.rt.width / hdCamera.screenSize.x,
                depth.rt.height / hdCamera.screenSize.y);
            cmd.SetGlobalVector("_BlitScaleBias", depth.scaleFactor);
            cmd.SetGlobalTexture("_InputDepthTexture", depth);
            cmd.Blit(depth, framebuffer.DepthTarget, copyDepthMaterial);

            // cmd.SetGlobalTexture("_InputTexture", framebuffer.DepthTarget);
            // cmd.SetGlobalTexture("_DepthTexture", framebuffer.DepthTarget);
            // cmd.Blit(framebuffer.DepthTarget, color, copyFramebufferMaterial, 0);

            OnPassExecute?.Invoke(renderContext, cmd, hdCamera.camera, framebuffer);
        }

        protected override void Cleanup()
        {
            OnPassCleanup?.Invoke();

            CoreUtils.Destroy(copyFramebufferMaterial);
            CoreUtils.Destroy(clearMaterial);
            CoreUtils.Destroy(copyDepthMaterial);

            RTHandles.Release(depthBuffer);
        }
    }
}
#endif