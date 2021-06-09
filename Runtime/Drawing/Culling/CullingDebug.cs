
using System;
using System.Collections.Generic;
using ReGizmo.Core;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CullingDebug
    {
        public struct BoundingBox
        {
            public Vector3 Center;
            public Vector3 Extents;
            public bool Visible;
        }

        ComputeBuffer buffer;

        public void Hook(CommandBuffer commandBuffer, ComputeShader compute, int kernelID, int len) 
        {
#if !UNITY_EDITOR
            return;
#endif

            if (buffer == null || buffer.Equals(null))
            {
                buffer = ComputeBufferPool.Get(len, System.Runtime.InteropServices.Marshal.SizeOf<CullingDebug.BoundingBox>(), name: "CullingDebugBuffer");
            }
            else if (buffer.count < len)
            {
                ComputeBufferPool.Free(buffer);
                buffer = ComputeBufferPool.Get(len, System.Runtime.InteropServices.Marshal.SizeOf<CullingDebug.BoundingBox>(), name: "CullingDebugBuffer");
            }

            commandBuffer.SetComputeBufferParam(compute, kernelID, "_DebugAABB", buffer);

            if (!Application.isPlaying || !ReGizmoSettings.ShowDebugGizmos) return;

            commandBuffer.RequestAsyncReadback(buffer, result =>  
            {
                if (result.hasError) return;

                var bbs = result.GetData<BoundingBox>();
                for (int i = 0; i < len; i++)
                {
                    var bb = bbs[i];
                    Color color = bb.Visible ? Color.green : Color.red;
                    ReDraw.AABBDebug(bb.Center, Quaternion.identity, bb.Extents, color);
                }
            });
        }

        public void CulledCount(CommandBuffer commandBuffer, int originalCount, ComputeBuffer argsBuffer)
        {
            commandBuffer.RequestAsyncReadback(argsBuffer, result =>
            {
                Debug.Log(result.GetData<uint>()[0] + " / " + originalCount);
            });
        }
    }
}