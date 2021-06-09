using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Utils
{
    internal class ShaderDataBuffer<T> : System.IDisposable
        where T : unmanaged
    {
        T[] shaderDataPool;
        ComputeBuffer shaderDataBuffer;
        T ghostData;

        volatile int writeCursor;

        public T[] ShaderDataPool => shaderDataPool;
        public ComputeBuffer ComputeBuffer => shaderDataBuffer;

        public ShaderDataBuffer(int capacity = 1024)
        {
            shaderDataPool = new T[0];
            Expand(capacity);

            writeCursor = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get()
        {
            // HACK: Kinda dirty, but we dont care about 100% accuracy in the chance that the buffer was resized
            try
            {
                if (writeCursor >= shaderDataPool.Length - 1) 
                {
                    lock (shaderDataPool)
                    {
                        Expand((int)(shaderDataPool.Length * 1.5f));
                    }
                }

                int pos = writeCursor;
                Interlocked.Increment(ref writeCursor);
                return ref shaderDataPool[pos];
            }
            catch
            {
                return ref ghostData;
            }
        }

        public RefRange<T> GetRange(int count)
        {
            // HACK: Kinda dirty, but we dont care about 100% accuracy in the chance that the buffer was resized
            try
            {
                if (writeCursor + count >= shaderDataPool.Length - 1) 
                {
                    lock (shaderDataPool)
                    {
                        Expand((int)(shaderDataPool.Length * 1.5f));
                    }
                }

                int start = writeCursor;
                Interlocked.Add(ref writeCursor, count);
                return new RefRange<T>(shaderDataPool, start, count);
            }
            catch
            {
                return RefRange<T>.Null();
            }
        }

        public void Reset()
        {
            writeCursor = 0;
        }

        public int Count()
        {
            return writeCursor;
        }

        public void Copy(ComputeArray<T> computeArray)
        {
            if (writeCursor + computeArray.Count >= shaderDataPool.Length)
            {
                Expand(computeArray.Count);
            }

            int count = computeArray.Copy(shaderDataPool, writeCursor);
            writeCursor += count;
        }

        public void PushData()
        {
            if (shaderDataBuffer == null || shaderDataBuffer.count < shaderDataPool.Length)
            {
                ComputeBufferPool.Free(shaderDataBuffer);
                shaderDataBuffer = ComputeBufferPool.Get(shaderDataPool.Length, Marshal.SizeOf<T>());
            }

            shaderDataBuffer.SetData(shaderDataPool, 0, 0, writeCursor);
        }

        void Expand(int amount)
        {
            var oldPool = shaderDataPool;

            int currentLength = shaderDataPool == null ? 0 : shaderDataPool.Length;
            shaderDataPool = new T[currentLength + amount];

            System.Array.Copy(oldPool, shaderDataPool, writeCursor);
        }

        public void Dispose()
        {
            shaderDataBuffer = ComputeBufferPool.Free(shaderDataBuffer);
        }
    }
}
