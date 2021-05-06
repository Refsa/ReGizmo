using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
#if RG_SRP
    class URPCommandBufferStack : CommandBufferStack
    {
        CommandBuffer current;

        public URPCommandBufferStack(string name) : base(name)
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