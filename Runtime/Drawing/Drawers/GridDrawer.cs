
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

    internal class GridDrawer : ReGizmoDrawer<GridData>
    {
        Mesh quad;

        public GridDrawer() : base()
        {
            quad = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Grid");
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth)
        {
            uniqueDrawData.SetVertexCount(quad.GetIndexCount(0));
            uniqueDrawData.SetInstanceCount(uniqueDrawData.DrawCount);

            if (depth)
            {
                cmd.DrawMeshInstancedIndirect(
                    quad, 0, material, 1,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
            else
            {
                cmd.DrawMeshInstancedIndirect(
                    quad, 0, material, 0,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
        }

        protected override void RenderWithPassInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, int pass)
        {
            uniqueDrawData.SetVertexCount(quad.GetIndexCount(0));
            uniqueDrawData.SetInstanceCount(uniqueDrawData.DrawCount);

            cmd.DrawMeshInstancedIndirect(
                quad, 0, material, pass,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);
        }
    }
}