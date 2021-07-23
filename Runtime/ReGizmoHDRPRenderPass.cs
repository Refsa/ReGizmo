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

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            name = "ReGizmoHDRPPass";

            clearMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/Clear");
            copyFramebufferMaterial = CoreUtils.CreateEngineMaterial("Hidden/ReGizmo/CopyFramebuffer");
        }

        protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
        {
            SetCameraRenderTarget(cmd);
            GetCameraBuffers(out var color, out var depth);
            var framebuffer = new Framebuffer(color, depth);

            OnPassExecute?.Invoke(renderContext, cmd, hdCamera.camera, framebuffer);
        }

        protected override void Cleanup()
        {
            OnPassCleanup?.Invoke();

            CoreUtils.Destroy(copyFramebufferMaterial);
            CoreUtils.Destroy(clearMaterial);
        }
    }
}
#endif