
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct GridData
    {
        public Vector3 Position1;
        public Vector3 Position2;
        public uint ID;
        public uint Index;
    }

    internal struct GridMeta
    {
        public float Range;
        public float Width;
        public Vector3 Normal;
        public Vector3 LineColor;
        public Vector3 Center;
    }

    internal class ReGizmoGridDrawer : ReGizmoDrawer<GridData>
    {
        ShaderDataBuffer<GridMeta> gridMetaBuffer;

        public ReGizmoGridDrawer() : base()
        {
            gridMetaBuffer = new ShaderDataBuffer<GridMeta>();

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Grid");
            renderArguments[1] = 1;
        }

        public ref GridMeta GetGridMetaData(out uint index)
        {
            index = (uint)gridMetaBuffer.Count();
            return ref gridMetaBuffer.Get();
        }

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();

            gridMetaBuffer.PushData(materialPropertyBlock, "_MetaData");
        }
    }
}