
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct GridData
    {
        public Vector3 Position1;
        public Vector3 Position2;
        public Vector3 Color;
        public float Width;
    }

    internal class ReGizmoGridDrawer : ReGizmoDrawer<GridData>
    {
        public ReGizmoGridDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Grid");
            renderArguments[1] = 1;
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
    }
}