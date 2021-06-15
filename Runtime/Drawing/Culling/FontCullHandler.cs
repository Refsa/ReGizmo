using ReGizmo.Core;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class FontCullingHandler : CullingHandler
    {
        static readonly int KernelID = CullingCompute.FindKernel("Font_CameraCulling");
        static readonly string InputID = "_FontInput";
        static readonly string OutputID = "_FontOutput";
        static readonly string TextDataID = "_FontTextData";

        public void SetData(CommandBuffer commandBuffer, ComputeBuffer textDataBuffer, ComputeBuffer charInfoBuffer)
        {
            if (textDataBuffer == null || charInfoBuffer == null ||
                !textDataBuffer.IsValid() || !charInfoBuffer.IsValid())
            {
                return;
            }

            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, "_FontTextData", textDataBuffer);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, "_FontCharacterInfos", charInfoBuffer);
        }

        public override void PerformCulling<TShaderData>(CommandBuffer commandBuffer, int drawCount, ComputeBuffer argsBuffer, int argsBufferOffset, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
        {
            if (inputBufer == null || outputBuffer == null ||
                !inputBufer.IsValid() || !outputBuffer.IsValid())
            {
                return;
            }

            outputBuffer.SetCounterValue(0);

#if REGIZMO_DEV
            cullingDebug.Hook(commandBuffer, CullingCompute, KernelID, drawCount);
#endif

            commandBuffer.SetComputeIntParam(CullingCompute, "_Count", drawCount);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, InputID, inputBufer);
            commandBuffer.SetComputeBufferParam(CullingCompute, KernelID, OutputID, outputBuffer);

            commandBuffer.DispatchCompute(CullingCompute, KernelID, Mathf.CeilToInt(drawCount / 128f), 1, 1);

            commandBuffer.CopyCounterValue(outputBuffer, argsBuffer, (uint)(sizeof(uint) * argsBufferOffset));
        }
    }
}