
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct MeshDrawerShaderData
    {
        public Vector3 Position;
        public Vector4 Rotation;
        public Vector3 Scale;
        public Vector4 Color;
    }

    internal class ReGizmoMeshDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;

        public ReGizmoMeshDrawer() : base() { }

        public ReGizmoMeshDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
            cullingHandler = new MeshCullingHandler();
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetInstanceCount(uniqueDrawData.DrawCount);
            uniqueDrawData.SetVertexCount(mesh.GetIndexCount(0));

            cmd.DrawMeshInstancedIndirect(
                mesh, 0, material, 0,
                uniqueDrawData.GetRenderArgsBuffer(), 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_Shaded", 0.35f);
            materialPropertyBlock.SetFloat("_FresnelFactor", 1f);
        }
    }
}