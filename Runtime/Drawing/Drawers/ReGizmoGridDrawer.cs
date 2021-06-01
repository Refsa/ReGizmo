
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct GridData
    {
        public Vector3 Position;
        public float Range;
        public Vector3 LineColor;
        public uint Flags;
    }

    internal enum GridMode : uint
    {
        Infinite = 1u << 0,
        Static = 1u << 1,
    }

    public enum GridPlane : uint
    {
        XZ = 1u << 10,
        XY = 1u << 11,
        ZY = 1u << 12,
    }

    internal class ReGizmoGridDrawer : ReGizmoDrawer<GridData>
    {
        Mesh quad;

        public ReGizmoGridDrawer() : base()
        {
            quad = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Grid");
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetVertexCount(quad.GetIndexCount(0));
            uniqueDrawData.SetInstanceCount(uniqueDrawData.DrawCount);

            cmd.DrawMeshInstancedIndirect(
                quad, 0, material, 0,
                uniqueDrawData.GetRenderArgsBuffer(), 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);
        }
    }
}