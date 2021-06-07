
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class AABBDebugDrawer : ReGizmoDrawer<LineData>
    {
        public AABBDebugDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Line_Screen");
        } 

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetInstanceCount(1);
            uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }
    }
}