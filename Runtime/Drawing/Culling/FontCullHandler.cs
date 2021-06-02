
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class FontCullingHandler : CullingHandler
    {
        static readonly int KernelID = CullingCompute.FindKernel("Font_CameraCulling"); 
        static readonly string InputID =  "_FontInput";
        static readonly string OutputID =  "_FontOutput";
        static readonly string TextDataID =  "_FontTextData";

        public void SetData(ComputeBuffer textDataBuffer)
        {
            CullingCompute.SetBuffer(KernelID, "_FontTextData", textDataBuffer);
        }

        public override int PerformCulling<TShaderData>(int drawCount, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
        {
            if (inputBufer == null || outputBuffer == null)
            {
                return 0;
            }

            CullingCompute.SetInt("_Count", drawCount);
            CullingCompute.SetBuffer(KernelID, InputID, inputBufer);
            CullingCompute.SetBuffer(KernelID, OutputID, outputBuffer);
            CullingCompute.Dispatch(KernelID, Mathf.CeilToInt(drawCount / 64f), 1, 1);

            ComputeBuffer.CopyCount(outputBuffer, countCopyBuffer, 0);
            countCopyBuffer.GetData(drawCounter);

            return drawCounter[0];
        }
    }
}