using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoMeshWireframeDrawer : ReGizmoDrawer<MeshDrawerShaderData>
    {
        protected Mesh mesh;

        public ReGizmoMeshWireframeDrawer() : base()
        {
            cullingHandler = new MeshCullingHandler();
        }

        public ReGizmoMeshWireframeDrawer(Mesh mesh) : base()
        {
            this.mesh = mesh;
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh_Wireframe");
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
        }
    }

    internal class ReGizmoCustomMeshWireframeDrawer : ReGizmoContentDrawer<ReGizmoMeshWireframeDrawer>
    {
        protected override IEnumerable<ReGizmoMeshWireframeDrawer> _drawers => drawers.Values;

        Dictionary<Mesh, ReGizmoMeshWireframeDrawer> drawers;

        public ReGizmoCustomMeshWireframeDrawer() : base()
        {
            drawers = new Dictionary<Mesh, ReGizmoMeshWireframeDrawer>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.GetShaderData();
        }

        ReGizmoMeshWireframeDrawer AddSubDrawer(Mesh mesh)
        {
            var drawer = new ReGizmoMeshWireframeDrawer(mesh);
            drawers.Add(mesh, drawer);
            return drawer;
        }
    }
}