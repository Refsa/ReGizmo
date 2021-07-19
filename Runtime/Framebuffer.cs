using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal struct Framebuffer
    {
        public RenderTargetIdentifier ColorTarget;
        public RenderTargetIdentifier DepthTarget;
    }
}