using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoMeshWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;
        uint indexCount;

        public ReGizmoMeshWireframeDrawer() : base()
        {
            cullingHandler = new MeshCullingHandler();
            argsBufferCountOffset = 1;
        }

        public ReGizmoMeshWireframeDrawer(Mesh mesh) : this()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh_Wireframe");
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
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
        }
    }

    internal class ReGizmoCustomMeshWireframeDrawer : ReGizmoContentDrawer<ReGizmoMeshWireframeDrawer>
    {
        protected override IEnumerable<(ReGizmoMeshWireframeDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Mesh, (ReGizmoMeshWireframeDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public ReGizmoCustomMeshWireframeDrawer() : base()
        {
            drawers = new Dictionary<Mesh, (ReGizmoMeshWireframeDrawer, UniqueDrawData)>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (ReGizmoMeshWireframeDrawer, UniqueDrawData) AddSubDrawer(Mesh mesh)
        {
            var drawer = new ReGizmoMeshWireframeDrawer(mesh);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(mesh, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}