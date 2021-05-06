using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
#if RG_SRP
    class SRPCommandBufferStack : CommandBufferStack
    {
        CommandBuffer current;

        public SRPCommandBufferStack(string name) : base(name)
        {
            current = new CommandBuffer();
            current.name = name;
        }

        public override CommandBuffer Current()
        {
            return current;
        }
    }
#endif
}