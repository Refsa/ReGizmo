
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct TriangleDrawData
    {
        public Vector3 Position;
        public Vector3 Normal;
        public float Width;
        public Vector3 Color;
        public int Flags;
    }

    internal class ReGizmoTriangleDrawer : ReGizmoDrawer<TriangleDrawData>
    {
        public ReGizmoTriangleDrawer() : base()
        {
            material = new Material(ReGizmoHelpers.LoadShader("Hidden/ReGizmo/TriangleShader"));
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