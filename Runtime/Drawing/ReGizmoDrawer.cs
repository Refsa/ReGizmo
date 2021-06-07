using System.Collections.Generic;
using ReGizmo.Core;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal interface IReGizmoDrawer
    {
        void Clear();
        void Dispose();
        void PushSharedData();
        void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData);
        uint CurrentDrawCount();
    }

    internal abstract class ReGizmoDrawer<TShaderData> : System.IDisposable, IReGizmoDrawer
        where TShaderData : unmanaged
    {
        protected virtual string PropertiesName { get; } = "_Properties";

        protected Material material;
        ShaderDataBuffer<TShaderData> shaderDataBuffer;
        int currentDrawCount;
        protected CullingHandler cullingHandler;
        protected int argsBufferCountOffset;

        public ReGizmoDrawer()
        {
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>();
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

        public void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;
            Profiler.BeginSample("ReGizmoDrawer::Render");

            if (cullingHandler != null)
            {
                var culledBuffer = uniqueDrawData.GetDrawBuffer<TShaderData>(currentDrawCount);
                cullingHandler.SetData(commandBuffer, cameraFrustum);
                SetCullingData(commandBuffer);

                cullingHandler.PerformCulling<TShaderData>(
                    commandBuffer,
                    currentDrawCount,
                    uniqueDrawData.ArgsBuffer, argsBufferCountOffset,
                    shaderDataBuffer.ComputeBuffer, culledBuffer);

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

            Profiler.EndSample();
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
        protected virtual void SetCullingData(CommandBuffer cmd) { }

        public virtual void Dispose()
        {
            shaderDataBuffer?.Dispose();
            cullingHandler?.Dispose();
        }
    }
}