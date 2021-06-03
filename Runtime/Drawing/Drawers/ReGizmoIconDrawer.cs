using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct IconShaderData
    {
        public Vector3 Position;
        public Vector3 Color;
        public float Scale;
        public int Flags;
    }

    internal class ReGizmoIconDrawer : ReGizmoDrawer<IconShaderData>
    {
        Texture2D icon;
        float aspect;

        protected override string PropertiesName { get; } = "_DrawData";

        public ReGizmoIconDrawer(Texture2D icon) : base()
        {
            this.icon = icon;
            this.aspect = (float)icon.width / (float)icon.height;

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Icon");

            cullingHandler = new IconCullingHandler();
            argsBufferCountOffset = 0;
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            // uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);
            uniqueDrawData.SetInstanceCount(1);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);

            materialPropertyBlock.SetTexture("_IconTexture", icon);
            materialPropertyBlock.SetFloat("_IconAspect", aspect);
        }
    }
}