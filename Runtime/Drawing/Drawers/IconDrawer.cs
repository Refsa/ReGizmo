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

    internal class IconDrawer : ReGizmoDrawer<IconShaderData>
    {
        Texture2D icon;
        float aspect;

        protected override string PropertiesName { get; } = "_DrawData";

        public IconDrawer(Texture2D icon) : base()
        {
            this.icon = icon;
            this.aspect = (float)icon.width / (float)icon.height;

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Icon");

            cullingHandler = new IconCullingHandler();
            argsBufferCountOffset = 0;
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

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);

            materialPropertyBlock.SetTexture("_IconTexture", icon);
            materialPropertyBlock.SetFloat("_IconAspect", aspect);
        }
    }
}