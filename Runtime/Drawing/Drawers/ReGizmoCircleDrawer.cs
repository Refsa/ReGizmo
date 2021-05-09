
using ReGizmo.Utility;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct CircleDrawData
    {
        public Vector3 Position;
        public Vector3 Normal;
        public float Radius;
        public float Thickness;
        public Vector3 Color;
    }

    internal class ReGizmoCircleDrawer : ReGizmoDrawer<CircleDrawData>
    {
        public ReGizmoCircleDrawer() : base()
        {
            material = new Material(ReGizmoHelpers.LoadShader("Hidden/ReGizmo/CircleShader"));
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