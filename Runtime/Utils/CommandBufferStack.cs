
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    class CommandBufferStack : IDisposable
    {
        protected string name;
        HashSet<CameraEvent> activeEvents;
        Dictionary<Camera, CommandBuffer> cameraCommandBuffers;

        public CommandBufferStack(string name)
        {
            this.name = name;
            activeEvents = new HashSet<CameraEvent>();
            cameraCommandBuffers = new Dictionary<Camera, CommandBuffer>();
        }

        public CommandBuffer Current(Camera camera)
        {
            if (!cameraCommandBuffers.TryGetValue(camera, out var cmd))
            {
                cmd = new CommandBuffer();
                cameraCommandBuffers.Add(camera, cmd);
            }
            
            return cmd;
        }

        public virtual void Dispose()
        {
            if (cameraCommandBuffers == null) return;

            foreach (var cmd in cameraCommandBuffers.Values)
            {
                cmd?.Release();
            }
        }

        public void Attach(Camera camera, CameraEvent cameraEvent)
        {
            activeEvents.Add(cameraEvent);

            camera.AddCommandBuffer(cameraEvent, Current(camera));
        }

        public void DeAttach(Camera camera)
        {
            if (camera == null || camera.Equals(null)) return;

            foreach (var ev in activeEvents)
            {
                camera.RemoveCommandBuffer(ev, Current(camera));
            }

            activeEvents.Clear();
        }

        public void DeAttach(Camera camera, CameraEvent cameraEvent)
        {
            if (activeEvents.Contains(cameraEvent))
            {
                camera.RemoveCommandBuffer(cameraEvent, Current(camera));

                activeEvents.Remove(cameraEvent);
            }
        }
    }
}