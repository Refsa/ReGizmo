using System.Collections.Generic;
using ReGizmo.Core;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal interface IReGizmoDrawer
    {
        void Clear();
        void Dispose();
        void PushSharedData();
        void Render(CommandBuffer cmd, CullingHandler cullingHandler);
        uint CurrentDrawCount();
    }

    class UniqueDrawData : System.IDisposable
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

    internal abstract class ReGizmoDrawer<TShaderData> : System.IDisposable, IReGizmoDrawer
        where TShaderData : unmanaged
    {
        protected static readonly Bounds DefaultRenderBounds = new Bounds(Vector3.zero, Vector3.one * 10_000f);

        protected virtual string PropertiesName { get; } = "_Properties";
        protected virtual CullingData CullingData { get; }

        protected Material material;
        ShaderDataBuffer<TShaderData> shaderDataBuffer;
        int currentDrawCount;

        Dictionary<CommandBuffer, UniqueDrawData> uniqueDrawDatas;

        protected Bounds currentBounds;

        public ReGizmoDrawer()
        {
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>();
            uniqueDrawDatas = new Dictionary<CommandBuffer, UniqueDrawData>();
            currentBounds = DefaultRenderBounds;
        }

        public ReGizmoDrawer(Material material) : this()
        {
            this.material = material;
        }

        public virtual void Clear()
        {
            shaderDataBuffer.Reset();
        }

        internal virtual ShaderDataBuffer<TShaderData> GetShaderDataBuffer()
        {
            return shaderDataBuffer;
        }

        public void PushSharedData()
        {
            currentDrawCount = shaderDataBuffer.Count();
            if (currentDrawCount == 0) return;
            shaderDataBuffer.PushData();
        }

        public void Render(CommandBuffer cmd, CullingHandler cullingHandler)
        {
            if (currentDrawCount == 0) return;

            if (!uniqueDrawDatas.TryGetValue(cmd, out var uniqueDrawData))
            {
                uniqueDrawData = new UniqueDrawData();
                uniqueDrawDatas.Add(cmd, uniqueDrawData);
            }

            if (CullingData != null)
            {
                var culledBuffer = uniqueDrawData.GetDrawBuffer<TShaderData>(currentDrawCount);
                uint culledCount = (uint)cullingHandler.SetCullingData<TShaderData>(CullingData, currentDrawCount, shaderDataBuffer.ComputeBuffer, culledBuffer);
                uniqueDrawData.SetDrawCount(culledCount);

                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, culledBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
                RenderInternal(cmd, uniqueDrawData);
            }
            else
            {
                uniqueDrawData.SetDrawCount((uint)currentDrawCount);
                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.ComputeBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
                RenderInternal(cmd, uniqueDrawData);
            }
        }

        public uint CurrentDrawCount()
        {
            return (uint)currentDrawCount;
        }

        public ref TShaderData GetShaderData()
        {
            return ref shaderDataBuffer.Get();
        }

        protected abstract void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData);
        protected virtual void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock) { }

        public virtual void Dispose()
        {
            foreach (var dd in uniqueDrawDatas.Values)
            {
                dd?.Dispose();
            }
            shaderDataBuffer?.Dispose();
        }
    }
}