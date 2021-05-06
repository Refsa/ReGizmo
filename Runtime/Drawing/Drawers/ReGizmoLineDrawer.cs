
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct LineData
    {
        public Vector3 Position; 
        public Vector4 Color;
        public float Width;
    }

    internal class ReGizmoLineDrawer : ReGizmoDrawer<LineData>
    {
        public ReGizmoLineDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Line_Screen");
            renderArguments[1] = 1;
        } 

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            // HACK: Doing "OIT" in two pass is a bad hack, but it works for now

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Lines,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 1,
                MeshTopology.Lines,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
        }
    }
}