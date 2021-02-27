using UnityEngine;



namespace ReGizmo.Drawing
{
    internal struct IconShaderData
    {
        public Vector3 Position;
        public Vector3 Color;
        public float Scale;
    }

    internal class ReGizmoIconDrawer : ReGizmoDrawer<IconShaderData>
    {
        Texture2D icon;

        public ReGizmoIconDrawer(Texture2D icon) : base()
        {
            this.icon = icon;

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Icon");
            renderArguments[1] = 1;
        }

        protected override void RenderInternal(Camera camera)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            Graphics.DrawProceduralIndirect(
                material, currentBounds, MeshTopology.Points,
                renderArgumentsBuffer, 0,
                camera, materialPropertyBlock);
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();
            materialPropertyBlock.SetTexture("_IconTexture", icon);
        }
    }
}