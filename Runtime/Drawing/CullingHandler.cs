
using ReGizmo.Core;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CullingData
    {
        public int KernelID;
        public string InputName;
        public string OutputName;
    }

    internal class CullingHandler
    {
        public static readonly ComputeShader CullingCompute;

        int[] drawCounter;
        ComputeBuffer countCopyBuffer;

        static CullingHandler()
        { 
            CullingCompute = ReGizmoHelpers.LoadCompute("Assets/ReGizmo/Runtime/Resources/Compute/CullCompute.compute");
        }

        public CullingHandler()
        {
            drawCounter = new int[1] { 0 };
            countCopyBuffer = ComputeBufferPool.Get(1, sizeof(int), ComputeBufferType.IndirectArguments);
        }

        public int SetCullingData<TShaderData>(CullingData cullingData, int drawCount, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
            where TShaderData : unmanaged
        {
            if (inputBufer == null || outputBuffer == null)
            {
                return 0;
            }

            outputBuffer.SetCounterValue(0);

            CullingCompute.SetInt("_Count", drawCount);
            CullingCompute.SetBuffer(cullingData.KernelID, cullingData.InputName, inputBufer);
            CullingCompute.SetBuffer(cullingData.KernelID, cullingData.OutputName, outputBuffer);
            CullingCompute.Dispatch(cullingData.KernelID, Mathf.CeilToInt(drawCount / 64f), 1, 1);

            ComputeBuffer.CopyCount(outputBuffer, countCopyBuffer, 0);
            countCopyBuffer.GetData(drawCounter);

            return drawCounter[0];
        }
    }
}