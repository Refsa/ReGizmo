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

        public class ReGizmoRenderPass : ScriptableRenderPass
        {
            RenderTargetIdentifier colorTarget;
            RenderTargetIdentifier depthTarget;

            public ReGizmoRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
            }

            public void Setup(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget)
            {
                this.colorTarget = colorTarget;
                this.depthTarget = depthTarget;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                bool gameView = !renderingData.cameraData.isSceneViewCamera;

                var framebuffer = new Framebuffer { ColorTarget = colorTarget, DepthTarget = depthTarget };

                if (renderingData.cameraData.isSceneViewCamera)
                {
                    framebuffer.ColorTarget = renderingData.cameraData.camera.activeTexture.colorBuffer;
                    framebuffer.DepthTarget = renderingData.cameraData.camera.activeTexture.depthBuffer;
                }

                OnPassExecute?.Invoke(
                    context,
                    renderingData.cameraData.camera,
                    framebuffer,
                    gameView);
            }
        }

        ReGizmoRenderPass renderPass;

        public override void Create()
        {
            renderPass = new ReGizmoRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderPass.Setup(renderer.cameraColorTarget, renderer.cameraDepth);
            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif