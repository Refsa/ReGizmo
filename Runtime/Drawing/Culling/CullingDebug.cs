
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
        }

        class BBDrawData
        {
            public int Count;
            public NativeArray<BoundingBox> BoundingBoxes;

            public BBDrawData(int len)
            {
                BoundingBoxes = new NativeArray<BoundingBox>(len, Allocator.Persistent);
            }

            public void EnsureSize(int len)
            {
                if (BoundingBoxes.Length < len)
                {
                    BoundingBoxes.Dispose();
                }

                BoundingBoxes = new NativeArray<BoundingBox>(len, Allocator.Persistent);
            }
        }

        ComputeBuffer buffer;

        public void Hook(CommandBuffer commandBuffer, ComputeShader compute, int kernelID, int len)
        {
            if (buffer == null || buffer.Equals(null))
            {
                buffer = ComputeBufferPool.Get(len, System.Runtime.InteropServices.Marshal.SizeOf<CullingDebug.BoundingBox>());
            }
            else if (buffer.count < len)
            {
                ComputeBufferPool.Free(buffer);
                buffer = ComputeBufferPool.Get(len, System.Runtime.InteropServices.Marshal.SizeOf<CullingDebug.BoundingBox>());
            }

            commandBuffer.SetComputeBufferParam(compute, kernelID, "_DebugAABB", buffer);
            commandBuffer.SetComputeIntParam(compute, "_Debug", 1);
            commandBuffer.RequestAsyncReadback(buffer, result =>
            {
                var bbs = result.GetData<BoundingBox>();
                for (int i = 0; i < len; i++)
                {
                    var bb = bbs[i];
                    ReDraw.WireCube(bb.Center, Quaternion.identity, bb.Extents, Color.white);
                }
            });
            // commandBuffer.SetComputeIntParam(compute, "_Debug", 0);
        }
    }
}