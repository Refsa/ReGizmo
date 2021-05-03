
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    class CommandBufferStack : IDisposable
    {
        CommandBuffer front;
        CommandBuffer back;

        bool useFront;

        HashSet<CameraEvent> activeEvents;

        public CommandBufferStack(string name)
        {
            useFront = true;
            activeEvents = new HashSet<CameraEvent>();

            front = new CommandBuffer();
            back = new CommandBuffer();

            front.name = name + "_front";
            back.name = name + "_back";
        }

        public CommandBuffer Current()
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
            target.Clear();
            return target;
        }

        public void Attach(Camera camera, CameraEvent cameraEvent)
        {
            activeEvents.Add(cameraEvent);

            camera.AddCommandBuffer(cameraEvent, front);
            camera.AddCommandBuffer(cameraEvent, back);
        }

        public void DeAttach(Camera camera)
        {
            foreach (var ev in activeEvents)
            {
                camera.RemoveCommandBuffer(ev, front);
                camera.RemoveCommandBuffer(ev, back);
            }

            activeEvents.Clear();
        }

        public void DeAttach(Camera camera, CameraEvent cameraEvent)
        {
            if (activeEvents.Contains(cameraEvent))
            {
                camera.RemoveCommandBuffer(cameraEvent, front);
                camera.RemoveCommandBuffer(cameraEvent, back);

                activeEvents.Remove(cameraEvent);
            }
        }

        public void Dispose()
        {
            /* if (front != null && !front.Equals(null))
            {
                front.Dispose();
            }

            if (back != null && !back.Equals(null))
            {
                back.Dispose();
            } */
        }
    }
}