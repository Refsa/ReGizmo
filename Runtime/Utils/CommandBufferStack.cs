
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    class CommandBufferStack : IDisposable
    {
        CommandBuffer front;
        CommandBuffer back;

        bool useFront;

        public CommandBufferStack(string name)
        {
            useFront = true;

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

        public void Attach(Camera camera, CameraEvent ev)
        {
            camera.AddCommandBuffer(ev, front);
            camera.AddCommandBuffer(ev, back);
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