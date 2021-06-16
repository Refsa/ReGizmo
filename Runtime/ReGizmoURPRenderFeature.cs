#if RG_URP
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ReGizmo.Core.URP
{
    public class ReGizmoURPRenderFeature : ScriptableRendererFeature
    {
        public static event Action<ScriptableRenderContext, Camera, bool> OnPassExecute;

        public class ReGizmoRenderPass : ScriptableRenderPass
        {
            public ReGizmoRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                bool gameView = !renderingData.cameraData.isSceneViewCamera;

                OnPassExecute?.Invoke(context, renderingData.cameraData.camera, gameView);
            }
        }

        ReGizmoRenderPass renderPass;

        public override void Create()
        {
            renderPass = new ReGizmoRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(renderPass);
        }
    }
}
#endif