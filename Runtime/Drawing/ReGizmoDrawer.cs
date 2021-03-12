
using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal interface IReGizmoDrawer
    {
        void Clear();
        void Dispose();
        void Render(Camera camera);
        uint CurrentDrawCount();
    }

    internal abstract class ReGizmoDrawer<TShaderData> : System.IDisposable, IReGizmoDrawer
        where TShaderData : unmanaged
    {
        protected static readonly Bounds DefaultRenderBounds = new Bounds(Vector3.zero, Vector3.one * 10_000f);

        protected virtual string PropertiesName { get; } = "_Properties";

        protected Material material;

        protected MaterialPropertyBlock materialPropertyBlock;
        protected uint[] renderArguments;
        protected ComputeBuffer renderArgumentsBuffer;

        ShaderDataBuffer<TShaderData> shaderDataBuffer;

        protected Bounds currentBounds;

        public ReGizmoDrawer()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            shaderDataBuffer = new ShaderDataBuffer<TShaderData>();

            renderArguments = new uint[5] { 0, 0, 0, 0, 0 };

            ComputeBufferPool.Free(renderArgumentsBuffer);
            renderArgumentsBuffer = ComputeBufferPool.Get(1, sizeof(uint) * 5, ComputeBufferType.IndirectArguments);

            currentBounds = DefaultRenderBounds;
        }

        public ReGizmoDrawer(Material material) : this()
        {
            this.material = material;
        }

        public void Clear()
        {
            shaderDataBuffer.Reset();
        }

        public virtual void Dispose()
        {
            renderArgumentsBuffer = ComputeBufferPool.Free(renderArgumentsBuffer);
            shaderDataBuffer?.Dispose();
        }

        public void Render(Camera camera)
        {
            if (shaderDataBuffer.Count() == 0) return;

            SetMaterialPropertyBlockData();
            RenderInternal(camera);
        }

        public uint CurrentDrawCount()
        {
            return (uint)shaderDataBuffer.Count();
        }

        public ref TShaderData GetShaderData()
        {
            return ref shaderDataBuffer.Get();
        }

        protected abstract void RenderInternal(Camera camera);

        protected virtual void SetMaterialPropertyBlockData()
        {
            shaderDataBuffer.PushData(materialPropertyBlock, PropertiesName);
        }
    }
}