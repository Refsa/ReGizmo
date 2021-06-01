
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct TriangleDrawData
    {
        public Vector3 Position;
        public Vector3 Normal;
        public float Width;
        public Vector4 Color;
        public int Flags;
    }

    internal class ReGizmoTriangleDrawer : ReGizmoDrawer<TriangleDrawData>
    {
        public ReGizmoTriangleDrawer() : base()
        {
            material = new Material(ReGizmoHelpers.LoadShader("Hidden/ReGizmo/TriangleShader"));
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetInstanceCount(1);
            uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                uniqueDrawData.GetRenderArgsBuffer(), 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }
    }
}