
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoPolyLineDrawer : ReGizmoDrawer<PolyLineData>
    {
        public ReGizmoPolyLineDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/PolyLine_Screen");
            renderArguments[1] = 1;
        }

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[0] = CulledDrawCount();
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