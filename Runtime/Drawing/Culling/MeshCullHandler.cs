

using System.Linq;
using ReGizmo.Core;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct MeshBoundingBox
    {
        public Vector3 Center;
        public Vector3 Size;
    }

    internal class MeshCullingHandler : CullingHandler
    {
        static readonly int KernelID = GetKernelID("Mesh_CameraCulling");
        static readonly string InputID = "_MeshInput";
        static readonly string OutputID = "_MeshOutput";

        public MeshBoundingBox BoundingBox { get; set; }

        public override void PerformCulling<TShaderData>(
            CommandBuffer commandBuffer, int drawCount,
            ComputeBuffer argsBuffer, int argsBufferOffset,
            ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
        {
            if (inputBufer == null || outputBuffer == null)
            {
                return;
            }
            outputBuffer.SetCounterValue(0);

#if REGIZMO_DEV
            cullingDebug.Hook(commandBuffer, CullingCompute, KernelID, drawCount);
#endif

            commandBuffer.SetComputeVectorParam(CullingCompute, "_BoundingBoxCenter", BoundingBox.Center);
            commandBuffer.SetComputeVectorParam(CullingCompute, "_BoundingBoxSize", BoundingBox.Size);
            
            commandBuffer.SetComputeIntParam(CullingCompute, "_Count", drawCount);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, InputID, inputBufer);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, OutputID, outputBuffer);
            commandBuffer.DispatchCompute(CullingCompute, KernelID, Mathf.CeilToInt(drawCount / 128f), 1, 1);

            commandBuffer.CopyCounterValue(outputBuffer, argsBuffer, (uint)(sizeof(uint) * argsBufferOffset));
        }
    }
}