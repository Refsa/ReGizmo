using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ReGizmo.Core.URP
{
#if RG_SRP
    public class ReGizmoRenderFeature : ScriptableRendererFeature
    {
        public static event Action<ScriptableRenderContext, bool> OnPassExecute;

        public class ReGizmoRenderPass : ScriptableRenderPass
        {
            public ReGizmoRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                bool gameView = !renderingData.cameraData.isSceneViewCamera;

                OnPassExecute?.Invoke(context, gameView);
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
#endif
}