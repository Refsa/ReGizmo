
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

    internal static class CullingHandler
    {
        public static readonly ComputeShader CullingCompute;

        static int[] drawCounter;
        static ComputeBuffer countCopyBuffer;

        static CullingHandler()
        {
            CullingCompute = ReGizmoHelpers.LoadCompute("Assets/ReGizmo/Runtime/Resources/Compute/CullCompute.compute");

            drawCounter = new int[1] { 0 };
            countCopyBuffer = ComputeBufferPool.Get(1, sizeof(int), ComputeBufferType.IndirectArguments);
        }

        public static int SetCullingData<TShaderData>(CullingData cullingData, int drawCount, ShaderDataBuffer<TShaderData> buffer)
            where TShaderData : unmanaged
        {
            buffer.CulledComputeBuffer.SetCounterValue(0);

            CullingCompute.SetInt("_Count", drawCount);
            CullingCompute.SetBuffer(cullingData.KernelID, cullingData.InputName, buffer.ComputeBuffer);
            CullingCompute.SetBuffer(cullingData.KernelID, cullingData.OutputName, buffer.CulledComputeBuffer);
            CullingCompute.Dispatch(cullingData.KernelID, Mathf.CeilToInt(drawCount / 64f), 1, 1);

            if (countCopyBuffer == null)
            {
                countCopyBuffer = ComputeBufferPool.Get(1, sizeof(int), ComputeBufferType.IndirectArguments);
            }

            ComputeBuffer.CopyCount(buffer.CulledComputeBuffer, countCopyBuffer, 0);
            countCopyBuffer.GetData(drawCounter);

            return drawCounter[0];
        }
    }
}