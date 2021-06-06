
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct LineData
    {
        public Vector3 Position1;
        public Vector3 Position2;
        public Vector3 Color;
        public float Width;
    }

    internal class ReGizmoLineDrawer : ReGizmoDrawer<LineData>
    {
        public ReGizmoLineDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Line_Screen");
            cullingHandler = new LineCullingHandler();
        } 

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetInstanceCount(1);

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