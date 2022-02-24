
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

    internal class MeshDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;
        protected MeshBoundingBox boundingBox;
        uint indexCount;

        public MeshDrawer() : base()
        {
            cullingHandler = new MeshCullingHandler();
            argsBufferCountOffset = 1;
        } 

        public MeshDrawer(Mesh mesh) : this()
        {
            this.mesh = mesh;

            boundingBox.Center = mesh.bounds.center;
            boundingBox.Size = mesh.bounds.size;
            (cullingHandler as MeshCullingHandler).BoundingBox = boundingBox;

            indexCount = mesh.GetIndexCount(0);

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth)
        {
            if (indexCount == 0)
            {
                indexCount = mesh.GetIndexCount(0);
            }
            uniqueDrawData.SetVertexCount(indexCount);

            if (depth)
            {
                cmd.DrawMeshInstancedIndirect(
                    mesh, 0, material, 1,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
            else
            {
                cmd.DrawMeshInstancedIndirect(
                    mesh, 0, material, 0,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
        }

        protected override void RenderWithPassInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, int pass)
        {
            if (indexCount == 0)
            {
                indexCount = mesh.GetIndexCount(0);
            }
            uniqueDrawData.SetVertexCount(indexCount);

            cmd.DrawMeshInstancedIndirect(
                mesh, 0, material, pass,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void RenderWithMaterialInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, Material material)
        {
            if (indexCount == 0)
            {
                indexCount = mesh.GetIndexCount(0);
            }

            uniqueDrawData.SetVertexCount(indexCount);

            cmd.DrawMeshInstancedIndirect(
                mesh, 0, material, 0,
                uniqueDrawData.ArgsBuffer, 0,
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