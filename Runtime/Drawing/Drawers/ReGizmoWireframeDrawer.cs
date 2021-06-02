using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;

        public ReGizmoWireframeDrawer() : base()
        {
            cullingHandler = new MeshCullingHandler();
        }

        public ReGizmoWireframeDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
            cullingHandler = new MeshCullingHandler();
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
            uniqueDrawData.SetVertexCount(mesh.GetIndexCount(0));
            uniqueDrawData.SetInstanceCount(uniqueDrawData.DrawCount);

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