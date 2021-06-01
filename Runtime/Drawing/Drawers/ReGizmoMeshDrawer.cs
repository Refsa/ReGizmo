
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
        static readonly CullingData cullingData = new CullingData
        {
            KernelID = CullingHandler.CullingCompute.FindKernel("Mesh_CameraCulling"),
            InputName = "_MeshInput",
            OutputName = "_MeshOutput"
        };

        protected Mesh mesh;
        protected override CullingData CullingData => cullingData;

        public ReGizmoMeshDrawer() : base() { }

        public ReGizmoMeshDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");

            renderArguments[0] = mesh.GetIndexCount(0);
        }

        protected override void RenderInternal(CommandBuffer cmd)
        {
            renderArguments[1] = CulledDrawCount();
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
        }
    }
}