using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class UniqueDrawData : System.IDisposable
    {
        uint[] args;
        uint drawCount;
        ComputeBuffer drawBuffer;
        MaterialPropertyBlock materialPropertyBlock;

        public ComputeBuffer ArgsBuffer;

        public uint DrawCount => drawCount;
        public MaterialPropertyBlock MaterialPropertyBlock => materialPropertyBlock;

        public UniqueDrawData()
        {
            args = new uint[5] { 0, 0, 0, 0, 0 };
            ArgsBuffer = ComputeBufferPool.Get(5, sizeof(uint), ComputeBufferType.IndirectArguments);
            materialPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetVertexCount(uint count)
        {
            args[0] = count;
        }

        public void SetInstanceCount(uint count)
        {
            args[1] = count;
        }

        public void SetDrawCount(uint count)
        {
            drawCount = count;
        }

        public ComputeBuffer GetDrawBuffer<TShaderData>(int size)
            where TShaderData : unmanaged
        {
            if (drawBuffer == null || drawBuffer.count < size)
            {
                if (drawBuffer == null)
                {
                    ComputeBufferPool.Free(drawBuffer);
                }

                drawBuffer = ComputeBufferPool.Get(size, System.Runtime.InteropServices.Marshal.SizeOf<TShaderData>(), ComputeBufferType.Append);
            }

            return drawBuffer;
        }

        public ComputeBuffer GetRenderArgsBuffer()
        {
            ArgsBuffer.SetData(args);
            return ArgsBuffer;
        }

        public void Dispose()
        {
            ComputeBufferPool.Free(ArgsBuffer);
            ComputeBufferPool.Free(drawBuffer);
        }
    }
}