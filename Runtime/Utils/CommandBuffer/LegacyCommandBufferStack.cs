using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
#if RG_LEGACY
    class LegacyCommandBufferStack : CommandBufferStack
    {
        CommandBuffer front;
        CommandBuffer back;

        bool useFront;

        HashSet<CameraEvent> activeEvents;

        public LegacyCommandBufferStack(string name) : base(name)
        {
            useFront = true;
            activeEvents = new HashSet<CameraEvent>();

            front = new CommandBuffer();
            back = new CommandBuffer();

            front.name = name + "_front";
            back.name = name + "_back";
        }

        public override CommandBuffer Current()
        {
            CommandBuffer target = null;

            if (useFront)
            {
                target = back;
            }
            else
            {
                target = front;
            }

            useFront = !useFront;
            return target;
        }

        public override void Attach(Camera camera, CameraEvent cameraEvent)
        {
            activeEvents.Add(cameraEvent);

            camera.AddCommandBuffer(cameraEvent, front);
            camera.AddCommandBuffer(cameraEvent, back);
        }

        public override void DeAttach(Camera camera)
        {
            if (camera == null || camera.Equals(null)) return;

            foreach (var ev in activeEvents)
            {
                camera.RemoveCommandBuffer(ev, front);
                camera.RemoveCommandBuffer(ev, back);
            }

            activeEvents.Clear();
        }

        public override void DeAttach(Camera camera, CameraEvent cameraEvent)
        {
            if (activeEvents.Contains(cameraEvent))
            {
                camera.RemoveCommandBuffer(cameraEvent, front);
                camera.RemoveCommandBuffer(cameraEvent, back);

                activeEvents.Remove(cameraEvent);
            }
        }
    }
#endif
}