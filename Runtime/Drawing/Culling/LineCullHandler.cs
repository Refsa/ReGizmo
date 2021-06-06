
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class LineCullingHandler : CullingHandler
    {
        static readonly int KernelID = CullingCompute.FindKernel("Line_CameraCulling");
        static readonly string InputID = "_LineInput";
        static readonly string OutputID = "_LineOutput";

        public override void PerformCulling<TShaderData>(
            CommandBuffer commandBuffer, int drawCount, 
            ComputeBuffer argsBuffer, int argsBufferOffset, 
            ComputeBuffer inputBuffer, ComputeBuffer outputBuffer)
        {
            if (inputBuffer == null || outputBuffer == null)
            {
                return;
            }
            outputBuffer.SetCounterValue(0);

            // FIXME: This recursively applies since lines are used to display the debug AABB
            cullingDebug.Hook(commandBuffer, CullingCompute, KernelID, drawCount);

            commandBuffer.SetComputeIntParam(CullingCompute, "_Count", drawCount);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, InputID, inputBuffer);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, OutputID, outputBuffer);
            commandBuffer.DispatchCompute(CullingCompute, KernelID, Mathf.CeilToInt(drawCount / 128f), 1, 1);

            commandBuffer.CopyCounterValue(outputBuffer, argsBuffer, (uint)(sizeof(uint) * argsBufferOffset));
        }
    }
}