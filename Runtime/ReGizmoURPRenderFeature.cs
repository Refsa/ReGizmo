#if RG_URP
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

namespace ReGizmo.Core.URP
{
    public class ReGizmoURPRenderFeature : ScriptableRendererFeature
    {
        internal delegate void PassExecuteDelegate(in ScriptableRenderContext context, Camera camera, in Framebuffer framebuffer, CommandBuffer commandBuffer);

        internal static event PassExecuteDelegate OnPassExecute;
        internal static event Action OnPassCleanup;

        class ReGizmoRenderPass : ScriptableRenderPass
        {
            RenderTargetIdentifier colorTarget;
            RenderTargetIdentifier depthTarget;

            public ReGizmoRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing + 100;
            }

            public void Setup(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget)
            {
                this.colorTarget = colorTarget;
                this.depthTarget = depthTarget;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cmd = CommandBufferPool.Get();
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                bool gameView = !renderingData.cameraData.isSceneViewCamera;
                ref var cameraData = ref renderingData.cameraData;

                colorTarget = cameraData.isSceneViewCamera ? colorAttachment : colorTarget;
                // colorTarget = colorAttachment;
                var framebuffer = new Framebuffer { ColorTarget = colorTarget, DepthTarget = depthTarget };

                OnPassExecute?.Invoke(
                    context,
                    cameraData.camera,
                    framebuffer,
                    cmd);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void FrameCleanup(CommandBuffer cmd)
            {
                OnPassCleanup?.Invoke();
            }
        }

        class DepthCopyPass : ScriptableRenderPass
        {
            RenderTargetHandle depthHandle;

            static Material blitDepthMaterial;

            public RenderTargetIdentifier DepthTarget => depthHandle.Identifier();

            public DepthCopyPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

                depthHandle.Init("_DepthCopy");
            }

#if UNITY_2020_3_OR_NEWER
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                var rtd = renderingData.cameraData.cameraTargetDescriptor;
                rtd.msaaSamples = 1;

                cmd.GetTemporaryRT(depthHandle.id, rtd, FilterMode.Point);
                ConfigureTarget(depthHandle.Identifier(), depthHandle.Identifier());
                ConfigureClear(ClearFlag.All, Color.clear);
            }
#else
            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                var rtd = cameraTextureDescriptor;
                rtd.msaaSamples = 1;

                cmd.GetTemporaryRT(depthHandle.id, rtd, FilterMode.Point);
                ConfigureTarget(depthHandle.Identifier(), depthHandle.Identifier());
                ConfigureClear(ClearFlag.All, Color.clear);
            }
#endif

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (blitDepthMaterial == null || blitDepthMaterial.Equals(null))
                {
                    blitDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/BlitDepth_URP"));
                }

                var cmd = CommandBufferPool.Get("ReGizmo::CopyDepth");
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                cmd.Blit(null, depthHandle.Identifier(), blitDepthMaterial);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

#if UNITY_2020_3_OR_NEWER
            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(depthHandle.id);
            }
#else
            public override void FrameCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(depthHandle.id);
            }
#endif
        }

        ReGizmoRenderPass renderPass;
        DepthCopyPass depthCopyPass;

        public override void Create()
        {
            depthCopyPass = new DepthCopyPass();
            renderPass = new ReGizmoRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!Core.ReGizmo.IsActive) return;

            renderer.EnqueuePass(depthCopyPass);

            renderPass.Setup(renderer.cameraColorTarget, depthCopyPass.DepthTarget);
            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif