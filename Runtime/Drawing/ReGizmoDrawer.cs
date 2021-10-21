using System;
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
        void RenderWithMaterial(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, Material material);
        void RenderWithPass(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, int pass);
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
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>(name: this.GetType().Name + "_Buffer");
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
            PushSharedDataInternal();
        }

        public void PreRender(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;
            Profiler.BeginSample("ReGizmoDrawer::PreRender");

            material.SetInt("_ZTest", (int)depthMode);

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
                if (argsBufferCountOffset == 0)
                {
                    uniqueDrawData.SetVertexCount((uint)currentDrawCount);
                }
                else if (argsBufferCountOffset == 1)
                {
                    uniqueDrawData.SetInstanceCount((uint)currentDrawCount);
                }
                uniqueDrawData.MaterialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.ComputeBuffer);
                SetMaterialPropertyBlockData(uniqueDrawData.MaterialPropertyBlock);
            }

            Profiler.EndSample();
        }

        public void RenderDepth(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDrawer::RenderDepth");
            RenderWithPassInternal(commandBuffer, uniqueDrawData, 1);
            Profiler.EndSample();
        }

        public void Render(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDrawer::Render");
            RenderInternal(commandBuffer, uniqueDrawData);
            Profiler.EndSample();
        }

        public void RenderWithMaterial(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, Material material)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDraw::RenderWithMaterial");
            RenderWithMaterialInternal(commandBuffer, uniqueDrawData, material);
            Profiler.EndSample();
        }

        public void RenderWithPass(CommandBuffer commandBuffer, CameraFrustum cameraFrustum, UniqueDrawData uniqueDrawData, int pass)
        {
            if (currentDrawCount == 0) return;

            Profiler.BeginSample("ReGizmoDrawer::Render");
            RenderWithPassInternal(commandBuffer, uniqueDrawData, pass);
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
        protected virtual void RenderWithPassInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, int pass) { }
        protected virtual void RenderWithMaterialInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, Material material) { }
        protected virtual void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock) { }
        protected virtual void PushSharedDataInternal() { }
        protected virtual void SetCullingData(CommandBuffer cmd) { }

        public virtual void Dispose()
        {
            shaderDataBuffer?.Dispose();
            cullingHandler?.Dispose();
        }
    }
}