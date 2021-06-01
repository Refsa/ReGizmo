
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    class CommandBufferStack : IDisposable
    {
        protected string name;
        CommandBuffer current;
        HashSet<CameraEvent> activeEvents;


        public CommandBufferStack(string name)
        {
            this.name = name;
            current = new CommandBuffer();
            activeEvents = new HashSet<CameraEvent>();
        }

        public CommandBuffer Current()
        {
            return current;
        }

        public virtual void Dispose()
        {
            current?.Release();
        }

        public void Attach(Camera camera, CameraEvent cameraEvent)
        {
            activeEvents.Add(cameraEvent);

            camera.AddCommandBuffer(cameraEvent, current);
        }

        public void DeAttach(Camera camera)
        {
            if (camera == null || camera.Equals(null)) return;

            foreach (var ev in activeEvents)
            {
                camera.RemoveCommandBuffer(ev, current);
            }

            activeEvents.Clear();
        }

        public void DeAttach(Camera camera, CameraEvent cameraEvent)
        {
            if (activeEvents.Contains(cameraEvent))
            {
                camera.RemoveCommandBuffer(cameraEvent, current);

                activeEvents.Remove(cameraEvent);
            }
        }
    }
}