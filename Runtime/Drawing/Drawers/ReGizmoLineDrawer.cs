
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal struct LineData
    {
        public Vector3 Position;
        public Vector4 Color;
        public float Width;
        public uint Mode;
    }

    internal class ReGizmoLineDrawer : ReGizmoDrawer<LineData>
    {
        public ReGizmoLineDrawer() : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIILine");
            renderArguments[1] = 1;
        }

        protected override void RenderInternal(Camera camera)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            Graphics.DrawProceduralIndirect(
                material, currentBounds, MeshTopology.Lines,
                renderArgumentsBuffer, 0,
                camera, materialPropertyBlock
            );
        }


    }
}