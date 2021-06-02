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
        void Render(CommandBuffer commandBuffer, CullingHandler cullingHandler, UniqueDrawData uniqueDrawData);
        uint CurrentDrawCount();
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

        protected Bounds currentBounds;

        public ReGizmoDrawer()
        {
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>();
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

        public void Render(CommandBuffer commandBuffer, CullingHandler cullingHandler, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;

            if (CullingData != null)
            {
                var culledBuffer = uniqueDrawData.GetDrawBuffer<TShaderData>(currentDrawCount);
                uint culledCount = (uint)cullingHandler
                    .PerformCulling<TShaderData>(CullingData, currentDrawCount, shaderDataBuffer.ComputeBuffer, culledBuffer);
                uniqueDrawData.SetDrawCount(culledCount);

                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, culledBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
                RenderInternal(commandBuffer, uniqueDrawData);
            }
            else
            {
                uniqueDrawData.SetDrawCount((uint)currentDrawCount);
                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.ComputeBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
                RenderInternal(commandBuffer, uniqueDrawData);
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
            shaderDataBuffer?.Dispose();
        }
    }
}