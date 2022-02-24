using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class PolyLineCullingHandler : CullingHandler
    {
        static readonly int KernelID = GetKernelID("PolyLine_CameraCulling");
        static readonly string InputID = "_PolyLineInput";
        static readonly string OutputID = "_PolyLineOutput";

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

#if REGIZMO_DEV
            cullingDebug.Hook(commandBuffer, CullingCompute, KernelID, drawCount);
#endif

            commandBuffer.SetComputeIntParam(CullingCompute, "_Count", drawCount);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, InputID, inputBuffer);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, OutputID, outputBuffer);
            commandBuffer.DispatchCompute(CullingCompute, KernelID, Mathf.CeilToInt(drawCount / 128f), 1, 1);

            commandBuffer.CopyCounterValue(outputBuffer, argsBuffer, (uint)(sizeof(uint) * argsBufferOffset));
        }
    }
}