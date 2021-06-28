using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        void PreRender(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData);
        void RenderDepth(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData);
        void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData);
        uint CurrentDrawCount();
        void SetDepthMode(DepthMode depthMode);
    }

    internal abstract class ReGizmoDrawer<TShaderData> : System.IDisposable, IReGizmoDrawer
        where TShaderData : unmanaged
    {
        protected virtual string PropertiesName { get; } = "_Properties";

        ShaderDataBuffer<TShaderData> shaderDataBuffer;
        int currentDrawCount;

        protected Material material;
        protected CullingHandler cullingHandler;
        protected int argsBufferCountOffset;
        protected DepthMode depthMode;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public void PreRender(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;
            Profiler.BeginSample("ReGizmoDrawer::PreRender");

            switch (depthMode)
            {
                case DepthMode.Sorted:
                    material.SetInt("_ZTest", (int)CompareFunction.LessEqual);
                    break;
                case DepthMode.Overlay:
                    material.SetInt("_ZTest", (int)CompareFunction.Always);
                    break;
            }

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
            }
            else
            {
                uniqueDrawData.SetDrawCount((uint)currentDrawCount);
                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.ComputeBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
            }

            Profiler.EndSample();
        }

        public void RenderDepth(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDrawer::RenderDepth");
            RenderInternal(commandBuffer, uniqueDrawData, true);
            Profiler.EndSample();
        }

        public void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDrawer::Render");
            RenderInternal(commandBuffer, uniqueDrawData);
            Profiler.EndSample();
        }

        public uint CurrentDrawCount()
        {
            return (uint)currentDrawCount;
        }

        public void SetDepthMode(DepthMode depthMode)
        {
            this.depthMode = depthMode;
        }

        // HACK: Just to avoid having duplicate ShaderDataBuffer:Get methods
        //       We cant pass the Get(out int count) into the void so we need
        //       to store it in an unused local
        uint _hack;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TShaderData GetShaderData()
        {
            return ref shaderDataBuffer.Get(out _hack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefRange<TShaderData> GetShaderDataRange(int count)
        {
            return shaderDataBuffer.GetRange(count);
        }

        protected abstract void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth = false);
        protected virtual void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock) { }
        protected virtual void SetCullingData(CommandBuffer cmd) { }

        public virtual void Dispose()
        {
            shaderDataBuffer?.Dispose();
            cullingHandler?.Dispose();
        }
    }
}