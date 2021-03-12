
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ShaderDataBuffer<T> : System.IDisposable
        where T : unmanaged
    {
        T[] shaderDataPool;
        ComputeBuffer shaderDataBuffer;

        int writeCursor;

        public T[] ShaderDataPool => shaderDataPool;

        public ShaderDataBuffer(int capacity = 1024)
        {
            Expand(capacity);

            writeCursor = 0;
        }

        public ref T Get()
        {
            if (writeCursor >= shaderDataPool.Length)
            {
                var oldPool = shaderDataPool;
                Expand(128);
                System.Array.Copy(oldPool, shaderDataPool, writeCursor);
            }

            return ref shaderDataPool[writeCursor++];
        }

        public void Reset()
        {
            writeCursor = 0;
        }

        public int Count()
        {
            return writeCursor;
        }

        public void PushData(MaterialPropertyBlock mpb, string name)
        {
            shaderDataBuffer.SetData(shaderDataPool, 0, 0, writeCursor);
            mpb.SetBuffer(name, shaderDataBuffer);
        }

        void Expand(int amount)
        {
            int currentLength = shaderDataPool == null ? 0 : shaderDataPool.Length;
            shaderDataPool = new T[currentLength + amount];

            ComputeBufferPool.Free(shaderDataBuffer);
            shaderDataBuffer = ComputeBufferPool.Get(shaderDataPool.Length, Marshal.SizeOf<T>());
        }

        public void Dispose()
        {
            shaderDataBuffer = ComputeBufferPool.Free(shaderDataBuffer);
        }
    }
}