
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct GridData
    {
        public Vector3 Position;
        public float Range;
        public Vector4 Orientation;
        public Vector3 LineColor;
        public uint Flags;
    }

    internal enum GridMode : uint
    {
        Infinite = 1u << 0,
        Static = 1u << 1,
    }

    internal class ReGizmoGridDrawer : ReGizmoDrawer<GridData>
    {
        Mesh quad;

        public ReGizmoGridDrawer() : base()
        {
            quad = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Grid");

            renderArguments[0] = quad.GetIndexCount(0);
        }

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[1] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            cmd.DrawMeshInstancedIndirect(
                quad, 0, material, 0,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();
        }
    }
}