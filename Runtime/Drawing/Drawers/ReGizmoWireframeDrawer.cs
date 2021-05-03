using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;
        public ReGizmoWireframeDrawer() : base() { }

        public ReGizmoWireframeDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");

            renderArguments[0] = mesh.GetIndexCount(0);
        }

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[1] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            cmd.DrawMeshInstancedIndirect(
                mesh, 0, material, 0,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();
            materialPropertyBlock.SetFloat("_Shaded", 0.35f);
            materialPropertyBlock.SetFloat("_FresnelFactor", 1f);
            materialPropertyBlock.SetFloat("_ZWrite", 0f);
        }
    }
}