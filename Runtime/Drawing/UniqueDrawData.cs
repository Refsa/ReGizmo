using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class UniqueDrawData : System.IDisposable
    {
        uint[] args;
        ComputeBuffer argsBuffer;
        uint drawCount;
        ComputeBuffer drawBuffer;
        MaterialPropertyBlock materialPropertyBlock;

        public ComputeBuffer ArgsBuffer => argsBuffer;
        public uint DrawCount => drawCount;
        public MaterialPropertyBlock MaterialPropertyBlock => materialPropertyBlock;

        public UniqueDrawData()
        {
            args = new uint[5] { 0, 0, 0, 0, 0 };
            argsBuffer = ComputeBufferPool.Get(5, sizeof(uint), ComputeBufferType.IndirectArguments);
            argsBuffer.SetData(args);

            materialPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetVertexCount(uint count)
        {
            args[0] = count;
            argsBuffer.SetData(args, 0, 0, 1);
        }

        public void SetInstanceCount(uint count)
        {
            args[1] = count;
            argsBuffer.SetData(args, 1, 1, 1);
        }

        public void SetDrawCount(uint count)
        {
            drawCount = count;
        }

        public ComputeBuffer GetRenderArgsBuffer() => argsBuffer;

        public ComputeBuffer GetDrawBuffer<TShaderData>(int size)
            where TShaderData : unmanaged
        {
            if (drawBuffer == null || drawBuffer.count < size)
            {
                if (drawBuffer != null && !drawBuffer.Equals(null))
                {
                    ComputeBufferPool.Free(drawBuffer);
                }

                drawBuffer = ComputeBufferPool.Get(size, System.Runtime.InteropServices.Marshal.SizeOf<TShaderData>(), ComputeBufferType.Append);
            }

            return drawBuffer;
        }

        public void Dispose()
        {
            ComputeBufferPool.Free(argsBuffer);
            ComputeBufferPool.Free(drawBuffer);
        }
    }
}