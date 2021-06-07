using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CustomMeshDrawer : ReGizmoContentDrawer<MeshDrawer>
    {
        protected override IEnumerable<(MeshDrawer, UniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Mesh, (MeshDrawer drawer, UniqueDrawData uniqueDrawData)> drawers;

        public CustomMeshDrawer() : base()
        {
            drawers = new Dictionary<Mesh, (MeshDrawer, UniqueDrawData)>();
        }

        public ref MeshDrawerShaderData GetShaderData(Mesh mesh)
        {
            if (!drawers.TryGetValue(mesh, out var drawer))
            {
                drawer = AddSubDrawer(mesh);
            }

            return ref drawer.drawer.GetShaderData();
        }

        (MeshDrawer, UniqueDrawData) AddSubDrawer(Mesh mesh)
        {
            var drawer = new MeshDrawer(mesh);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(mesh, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}