
using System.Collections.Generic;
using ReGizmo.Utils;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class TexturesDrawer : ReGizmoContentDrawer<TextureDrawer>
    {
        protected override IEnumerable<(TextureDrawer drawer, UniqueDrawData uniqueDrawData)> _drawers => drawers.Values;

        Dictionary<Texture, (TextureDrawer, UniqueDrawData)> drawers;
        Mesh quadMesh;

        public TexturesDrawer() : base()
        {
            quadMesh = ReGizmoPrimitives.Quad();
            drawers = new Dictionary<Texture, (TextureDrawer, UniqueDrawData)>();
        }

        public ref MeshDrawerShaderData GetShaderData(Texture texture)
        {
            if (!drawers.TryGetValue(texture, out var drawer))
            {
                drawer = AddSubDrawer(texture);
            }

            return ref drawer.Item1.GetShaderData();
        }

        (TextureDrawer, UniqueDrawData) AddSubDrawer(Texture texture)
        {
            var drawer = new TextureDrawer(quadMesh, texture);
            drawer.SetDepthMode(depthMode);
            var uniqueDrawData = new UniqueDrawData();

            drawers.Add(texture, (drawer, uniqueDrawData));
            return (drawer, uniqueDrawData);
        }
    }
}