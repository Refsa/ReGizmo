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
        void Render(CommandBuffer cmd);
        uint CurrentDrawCount();
    }

    internal abstract class ReGizmoDrawer<TShaderData> : System.IDisposable, IReGizmoDrawer
        where TShaderData : unmanaged
    {
        protected static readonly Bounds DefaultRenderBounds = new Bounds(Vector3.zero, Vector3.one * 10_000f);

        protected virtual string PropertiesName { get; } = "_Properties";
        protected virtual CullingData CullingData { get; }
        readonly string typeName;

        protected Material material;

        protected MaterialPropertyBlock materialPropertyBlock;
        protected uint[] renderArguments;
        protected ComputeBuffer renderArgumentsBuffer;

        ShaderDataBuffer<TShaderData> shaderDataBuffer;
        int culledDrawCount;

        protected Bounds currentBounds;

        public ReGizmoDrawer()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>();

            renderArguments = new uint[5] { 0, 0, 0, 0, 0 };

            ComputeBufferPool.Free(renderArgumentsBuffer);
            renderArgumentsBuffer = ComputeBufferPool.Get(1, sizeof(uint) * 5, ComputeBufferType.IndirectArguments);

            currentBounds = DefaultRenderBounds;

            typeName = this.GetType().Name;
        }

        public ReGizmoDrawer(Material material) : this()
        {
            this.material = material;
        }

        public virtual void Clear()
        {
            shaderDataBuffer.Reset();
        }

        public virtual void Dispose()
        {
            renderArgumentsBuffer = ComputeBufferPool.Free(renderArgumentsBuffer);
            shaderDataBuffer?.Dispose();
        }

        internal virtual ShaderDataBuffer<TShaderData> GetShaderDataBuffer()
        {
            return shaderDataBuffer;
        }

        public void Render(CommandBuffer cmd)
        {
            int currentDrawCount = shaderDataBuffer.Count();
            if (currentDrawCount == 0) return;

            shaderDataBuffer.PushData();

            if (CullingData != null)
            {
                culledDrawCount = CullingHandler.SetCullingData<TShaderData>(CullingData, currentDrawCount, shaderDataBuffer);

                materialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.CulledComputeBuffer);
                SetMaterialPropertyBlockData();
                RenderInternal(cmd);
            }
            else
            {
                culledDrawCount = currentDrawCount;
                materialPropertyBlock.SetBuffer(PropertiesName, shaderDataBuffer.ComputeBuffer);
                SetMaterialPropertyBlockData();
                RenderInternal(cmd);
            }
        }

        public uint CurrentDrawCount()
        {
            return (uint)shaderDataBuffer.Count();
        }

        public uint CulledDrawCount()
        {
            return (uint)culledDrawCount;
        }

        public ref TShaderData GetShaderData()
        {
            return ref shaderDataBuffer.Get();
        }

        protected abstract void RenderInternal(CommandBuffer cmd);
        protected virtual void SetMaterialPropertyBlockData() { }
    }
}