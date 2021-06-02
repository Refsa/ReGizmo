

using System.Linq;
using ReGizmo.Core;
using UnityEngine;
using UnityEngine.Profiling;

namespace ReGizmo.Drawing
{
    internal class MeshCullingHandler : CullingHandler
    {
        static readonly int KernelID = CullingCompute.FindKernel("Mesh_CameraCulling");
        static readonly string InputID = "_MeshInput";
        static readonly string OutputID = "_MeshOutput";

        public override void PerformCulling<TShaderData>(int drawCount, ComputeBuffer argsBuffer, int argsBufferOffset, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
        {
            if (inputBufer == null || outputBuffer == null)
            {
                return;
            }
            Profiler.BeginSample("ReGizmo::Culling::Cull");

            outputBuffer.SetCounterValue(0);

            CullingCompute.SetInt("_Count", drawCount);
            CullingCompute.SetBuffer(KernelID, InputID, inputBufer);
            CullingCompute.SetBuffer(KernelID, OutputID, outputBuffer);
            CullingCompute.Dispatch(KernelID, Mathf.CeilToInt(drawCount / 128f), 1, 1);

            ComputeBuffer.CopyCount(outputBuffer, argsBuffer, sizeof(uint) * argsBufferOffset);

            Profiler.EndSample();
        }
    }
}