using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCustomMeshDrawer : ReGizmoContentDrawer<ReGizmoMeshDrawer>
    {
        protected override IEnumerable<(ReGizmoMeshDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Mesh, (ReGizmoMeshDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public ReGizmoCustomMeshDrawer() : base()
        {
            drawers = new Dictionary<Mesh, (ReGizmoMeshDrawer, UniqueDrawData)>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (ReGizmoMeshDrawer, UniqueDrawData) AddSubDrawer(Mesh mesh)
        {
            var drawer = new ReGizmoMeshDrawer(mesh);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(mesh, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}