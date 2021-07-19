#if RG_URP
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ReGizmo.Core.URP
{
    public class ReGizmoURPRenderFeature : ScriptableRendererFeature
    {
        internal static event Action<ScriptableRenderContext, Camera, Framebuffer, bool> OnPassExecute;

        class ReGizmoRenderPass : ScriptableRenderPass
        {
            RenderTargetIdentifier colorTarget;

            DepthCopyPass depthCopyPass;

            public ReGizmoRenderPass(DepthCopyPass depthCopyPass)
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing + 399;
                this.depthCopyPass = depthCopyPass;
            }

            public void Setup(RenderTargetIdentifier colorTarget)
            {
                this.colorTarget = colorTarget;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                bool gameView = !renderingData.cameraData.isSceneViewCamera;

                var framebuffer = new Framebuffer { ColorTarget = colorTarget, DepthTarget = depthCopyPass.DepthTarget };

                ref var cameraData = ref renderingData.cameraData;

                if (renderingData.cameraData.isSceneViewCamera)
                {
                    framebuffer.ColorTarget = cameraData.targetTexture.colorBuffer;
                    framebuffer.DepthTarget = depthCopyPass.DepthTarget;
                }

                OnPassExecute?.Invoke(
                    context,
                    renderingData.cameraData.camera,
                    framebuffer,
                    gameView);
            }
        }

        class DepthCopyPass : ScriptableRenderPass
        {
            RenderTargetHandle depthHandle;
            RenderTargetIdentifier depthTarget;

            public RenderTargetIdentifier DepthTarget => depthHandle.Identifier();

            public DepthCopyPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;

                depthHandle.Init("Depth");
            }

            public void Setup(RenderTargetIdentifier depthTarget)
            {
                this.depthTarget = depthTarget;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                var rtd = cameraTextureDescriptor;
                rtd.colorFormat = RenderTextureFormat.Depth;
                rtd.depthBufferBits = 24;
                rtd.msaaSamples = 1;

                cmd.GetTemporaryRT(depthHandle.id, rtd);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cmd = CommandBufferPool.Get("ReGizmo::CopyDepth");
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                depthTarget = renderingData.cameraData.isSceneViewCamera ? depthAttachment : depthTarget;

                cmd.Blit(depthTarget, depthHandle.Identifier());

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void FrameCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(depthHandle.id);
            }
        }

        ReGizmoRenderPass renderPass;
        DepthCopyPass depthCopyPass;

        public override void Create()
        {
            depthCopyPass = new DepthCopyPass();
            renderPass = new ReGizmoRenderPass(depthCopyPass);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            depthCopyPass.Setup(renderer.cameraDepth);
            renderPass.Setup(renderer.cameraColorTarget);
            
            renderer.EnqueuePass(depthCopyPass);
            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif