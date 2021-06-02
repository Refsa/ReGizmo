using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Core
{
    [System.Serializable]
    public class ComputeBufferPool : System.IDisposable
    {
        static ComputeBufferPool instance;
        public static ComputeBufferPool Instance => instance;

        public static ComputeBufferPool Init()
        {
            if (instance != null) FreeAll();

            instance = new ComputeBufferPool();
            return instance;
        }

        List<ComputeBuffer> activeBuffers = new List<ComputeBuffer>();

        public static ComputeBuffer Get(int count, int stride, ComputeBufferType type = ComputeBufferType.Default)
        {
            var cb = new ComputeBuffer(count, stride, type);
            instance.activeBuffers.Add(cb);
            return cb;
        }

        public static ComputeBuffer Free(ComputeBuffer buffer)
        {
            if (buffer == null || !buffer.IsValid()) return null;

            instance.activeBuffers.Remove(buffer);
            buffer.Dispose();

            return null;
        }

        public static void FreeAll()
        {
            if (instance == null || instance.activeBuffers == null) return;

            foreach (var cb in instance.activeBuffers)
            {
                cb.Dispose();
            }

            instance.activeBuffers.Clear();
        }

        public void Dispose()
        {
            FreeAll();
        }
    }
}