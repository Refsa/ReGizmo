

using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class MeshCullingHandler : CullingHandler
    {
        static readonly int KernelID = CullingHandler.CullingCompute.FindKernel("Mesh_CameraCulling");

        static readonly string InputID = "_MeshInput";
        static readonly string OutputID = "_MeshOutput";

        public override int PerformCulling<TShaderData>(int drawCount, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
        {
            if (inputBufer == null || outputBuffer == null)
            {
                return 0;
            }

            outputBuffer.SetCounterValue(0);

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