using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        static readonly CullingData cullingData = new CullingData
        {
            KernelID = CullingHandler.CullingCompute.FindKernel("Mesh_CameraCulling"),
            InputName = "_MeshInput",
            OutputName = "_MeshOutput"
        };

        protected Mesh mesh;

        protected override CullingData CullingData => cullingData;

        public ReGizmoWireframeDrawer() : base() { }

        public ReGizmoWireframeDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
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