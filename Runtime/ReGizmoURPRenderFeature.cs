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
        internal static event Action<ScriptableRenderContext, Camera, Framebuffer, bool> OnPassExecute;

        class ReGizmoRenderPass : ScriptableRenderPass
        {
            RenderTargetIdentifier colorTarget;

            DepthCopyPass depthCopyPass;
            public RenderTargetHandle DepthHandle;

            public ReGizmoRenderPass(DepthCopyPass depthCopyPass)
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
                this.depthCopyPass = depthCopyPass;

                DepthHandle.Init("_DepthCopy");
            }

            public void Setup(RenderTargetIdentifier colorTarget)
            {
                this.colorTarget = colorTarget;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                bool gameView = !renderingData.cameraData.isSceneViewCamera;

                var framebuffer = new Framebuffer { ColorTarget = colorTarget, DepthTarget = DepthHandle.Identifier() };

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

            static Material blitDepthMaterial;

            public RenderTargetIdentifier DepthTarget => depthHandle.Identifier();

            public DepthCopyPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing - 1;

                depthHandle.Init("Depth");

                if (blitDepthMaterial == null)
                {
                    blitDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/BlitDepth"));
                }
            }

            public void Setup(RenderTargetIdentifier depthTarget)
            {
                this.depthTarget = depthTarget;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                var rtd = cameraTextureDescriptor;
                rtd.colorFormat = RenderTextureFormat.Depth;
                rtd.depthBufferBits = 32;
                rtd.msaaSamples = 1;

                cmd.GetTemporaryRT(depthHandle.id, rtd, FilterMode.Point);

                ConfigureTarget(depthHandle.Identifier());
                ConfigureClear(ClearFlag.None, Color.black);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cmd = CommandBufferPool.Get("ReGizmo::CopyDepth");
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                depthTarget = renderingData.cameraData.isSceneViewCamera ? depthAttachment : depthTarget;

                // cmd.SetGlobalTexture("_CameraDepthAttachment", depthTarget);
                cmd.Blit(depthTarget, depthHandle.Identifier(), blitDepthMaterial);

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

        CopyDepthPass copyDepthPass;
        RenderTargetHandle depthHandle;

        public override void Create()
        {
            depthCopyPass = new DepthCopyPass();
            copyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingPostProcessing - 1, new Material(Shader.Find("Hidden/Universal Render Pipeline/CopyDepth")));
            renderPass = new ReGizmoRenderPass(depthCopyPass);

            depthHandle.Init("_ReGizmoDepth");
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!Core.ReGizmo.IsActive) return;

            renderPass.Setup(renderer.cameraColorTarget);

            if (renderingData.cameraData.isSceneViewCamera)
            {
                depthCopyPass.Setup(renderer.cameraDepthTarget);
                renderer.EnqueuePass(depthCopyPass);
            }
            else
            {
                copyDepthPass.Setup(depthHandle, renderPass.DepthHandle);
                renderer.EnqueuePass(copyDepthPass);
            }

            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif