
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

    internal class LineDrawer : ReGizmoDrawer<LineData>
    {
        public LineDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Line_Screen");
            cullingHandler = new LineCullingHandler();
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth)
        {
            uniqueDrawData.SetInstanceCount(1);

            if (depth)
            {
                cmd.DrawProceduralIndirect(
                    Matrix4x4.identity,
                    material, 1,
                    MeshTopology.Points,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
            else
            {
                cmd.DrawProceduralIndirect(
                    Matrix4x4.identity,
                    material, 0,
                    MeshTopology.Points,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
        }

        protected override void RenderWithPassInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, int pass)
        {
            uniqueDrawData.SetInstanceCount(1);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, pass,
                MeshTopology.Points,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }
    }
}