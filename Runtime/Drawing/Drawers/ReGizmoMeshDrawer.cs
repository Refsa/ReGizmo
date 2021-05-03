
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct MeshDrawerShaderData
    {
        public Vector3 Position;
        public Vector3 Rotation;
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

            /* Graphics.DrawMeshInstancedIndirect(
                mesh, 0, material,
                currentBounds,
                renderArgumentsBuffer, 0, materialPropertyBlock,
                ShadowCastingMode.Off, false,
                0, camera
            ); */
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