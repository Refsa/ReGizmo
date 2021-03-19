using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCustomMeshDrawer : ReGizmoContentDrawer<ReGizmoMeshDrawer>
    {
        protected override IEnumerable<ReGizmoMeshDrawer> _drawers => drawers.Values;

        Dictionary<Mesh, ReGizmoMeshDrawer> drawers;

        public ReGizmoCustomMeshDrawer() : base()
        {
            drawers = new Dictionary<Mesh, ReGizmoMeshDrawer>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.GetShaderData();
        }

        ReGizmoMeshDrawer AddSubDrawer(Mesh mesh)
        {
            var drawer = new ReGizmoMeshDrawer(mesh);
            drawers.Add(mesh, drawer);
            return drawer;
        }
    }
}