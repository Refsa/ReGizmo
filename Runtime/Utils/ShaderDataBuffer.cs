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
        Mutex mutex;

        public ComputeBuffer ComputeBuffer => shaderDataBuffer;

        public ShaderDataBuffer(int capacity = 1024)
        {
            Expand(capacity);

            mutex = new Mutex();

            writeCursor = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(out uint atCount)
        {
            // HACK: Kinda dirty, but we dont care about 100% accuracy in the chance that the buffer was resized
            int pos = Interlocked.Increment(ref writeCursor);
            EnsureCapacity(pos);
            pos -= 1;
            atCount = (uint)pos;
            return ref shaderDataPool[pos];
        }

        public RefRange<T> GetRange(int count)
        {
            // HACK: Kinda dirty, but we dont care about 100% accuracy in the chance that the buffer was resized
            int end = Interlocked.Add(ref writeCursor, count);
            EnsureCapacity(end);

            int start = end - count;
            return new RefRange<T>(shaderDataPool, start, end);
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

        void EnsureCapacity(int capacity)
        {
            if (capacity >= shaderDataPool.Length - 1)
            {
                lock (shaderDataPool)
                {
                    if (capacity < shaderDataPool.Length - 1) return;

                    Expand((int)(shaderDataPool.Length * 1.5f));
                }
            }
        }

        void Expand(int amount)
        {
            var oldPool = shaderDataPool;

            int currentLength = shaderDataPool == null ? 0 : shaderDataPool.Length;
            shaderDataPool = new T[currentLength + amount];

            if (oldPool != null)
            {
                System.Array.Copy(oldPool, shaderDataPool, oldPool.Length);
            }
        }

        public void Dispose()
        {
            shaderDataBuffer = ComputeBufferPool.Free(shaderDataBuffer);
        }
    }
}
