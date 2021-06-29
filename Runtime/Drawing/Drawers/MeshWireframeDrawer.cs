using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class MeshWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;
        uint indexCount;

        public MeshWireframeDrawer() : base()
        {
            cullingHandler = new MeshCullingHandler();
            argsBufferCountOffset = 1;
        }

        public MeshWireframeDrawer(Mesh mesh) : this()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh_Wireframe");
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

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);
        }
    }

    internal class CustomMeshWireframeDrawer : ReGizmoContentDrawer<MeshWireframeDrawer>
    {
        protected override IEnumerable<(MeshWireframeDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Mesh, (MeshWireframeDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public CustomMeshWireframeDrawer() : base()
        {
            drawers = new Dictionary<Mesh, (MeshWireframeDrawer, UniqueDrawData)>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (MeshWireframeDrawer, UniqueDrawData) AddSubDrawer(Mesh mesh)
        {
            var drawer = new MeshWireframeDrawer(mesh);
            drawer.SetDepthMode(depthMode);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(mesh, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}