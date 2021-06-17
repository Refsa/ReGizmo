#if RG_HDRP
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace ReGizmo.Core.HDRP
{
    class ReGizmoHDRPRenderPass : CustomPass
    {
        public static event System.Action<CommandBuffer, Camera> OnPassExecute;
        public static event System.Action OnPassCleanup;
    
        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
        
        }
    
        protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
        {
            OnPassExecute?.Invoke(cmd, hdCamera.camera);
        }
    
        protected override void Cleanup()
        {
            OnPassCleanup?.Invoke();
        }
    }
}
#endif