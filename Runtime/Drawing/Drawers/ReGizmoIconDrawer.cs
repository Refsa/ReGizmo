using UnityEngine;
using UnityEngine.Rendering;

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
        float aspect;

        public ReGizmoIconDrawer(Texture2D icon) : base()
        {
            this.icon = icon;
            this.aspect = (float)icon.width / (float)icon.height;

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Icon");
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

            /* Graphics.DrawProceduralIndirect(
                material, currentBounds, MeshTopology.Points,
                renderArgumentsBuffer, 0,
                camera, materialPropertyBlock); */
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();

            materialPropertyBlock.SetTexture("_IconTexture", icon);
            materialPropertyBlock.SetFloat("_IconAspect", aspect);
        }
    }
}