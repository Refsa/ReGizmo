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
        internal static event Action<ScriptableRenderContext, Camera, Framebuffer, CommandBuffer> OnPassExecute;

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
                var framebuffer = new Framebuffer { ColorTarget = colorTarget, DepthTarget = depthTarget };

                OnPassExecute?.Invoke(
                    context,
                    renderingData.cameraData.camera,
                    framebuffer,
                    cmd);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
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
                rtd.depthBufferBits = 32;
                rtd.msaaSamples = 1;

                cmd.GetTemporaryRT(depthHandle.id, rtd, FilterMode.Point);

                ConfigureTarget(new RenderTargetIdentifier(depthHandle.Identifier(), 0, CubemapFace.Unknown, -1));
                ConfigureClear(ClearFlag.None, Color.black);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (blitDepthMaterial == null || blitDepthMaterial.Equals(null))
                {
                    blitDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/BlitDepth_URP"));
                    // blitDepthMaterial = new Material(Shader.Find("Hidden/Universal Render Pipeline/CopyDepth"));
                }

                var cmd = CommandBufferPool.Get("ReGizmo::CopyDepth");
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                ref var cameraData = ref renderingData.cameraData;

                cmd.SetGlobalTexture("_CameraDepthAttachment", depthAttachment);

                // cmd.Blit(depthTarget, depthHandle.Identifier(), blitDepthMaterial);
                cmd.DrawMesh(RenderingUtils.fullscreenMesh, cameraData.camera.transform.localToWorldMatrix, blitDepthMaterial);

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
            // copyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingPostProcessing - 1, new Material(Shader.Find("Hidden/Universal Render Pipeline/CopyDepth")));
            renderPass = new ReGizmoRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!Core.ReGizmo.IsActive) return;

            depthCopyPass.Setup(renderer.cameraDepthTarget);
            renderer.EnqueuePass(depthCopyPass);

            renderPass.Setup(renderer.cameraColorTarget, depthCopyPass.DepthTarget);
            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif