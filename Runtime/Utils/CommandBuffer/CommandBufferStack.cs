
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    abstract class CommandBufferStack : IDisposable
    {
        protected string name;

        public CommandBufferStack(string name)
        {
            this.name = name;
        }

        public abstract CommandBuffer Current();

#if RG_LEGACY
        public abstract void Attach(Camera camera, CameraEvent cameraEvent);
        public abstract void DeAttach(Camera camera);
        public abstract void DeAttach(Camera camera, CameraEvent cameraEvent);
#elif RG_HDRP
        public abstract void OnEndFrameRendering(ScriptableRenderContext context, Camera[] cameras);
#endif

        public virtual void Dispose() { }
    }
}