using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal struct Framebuffer
    {
#if RG_HDRP
        public RTHandle ColorTarget;
        public RTHandle DepthTarget;

        public Framebuffer(RTHandle colorTarget, RTHandle depthTarget)
        {
            ColorTarget = colorTarget;
            DepthTarget = depthTarget;
        }
#else
        public RenderTargetIdentifier ColorTarget;
        public RenderTargetIdentifier DepthTarget;

        public Framebuffer(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget)
        {
            ColorTarget = colorTarget;
            DepthTarget = depthTarget;
        }
#endif

    }
}