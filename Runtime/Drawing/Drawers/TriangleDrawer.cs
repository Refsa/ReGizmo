
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

    internal class TriangleDrawer : ReGizmoDrawer<TriangleDrawData>
    {
        public TriangleDrawer() : base()
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
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }
    }
}