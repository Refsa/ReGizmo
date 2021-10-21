
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class TextureDrawer : MeshDrawer
    {
        Texture texture;

        public TextureDrawer(Mesh mesh, Texture texture) : base(mesh)
        {
            this.texture = texture;
            this.material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Texture");
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            if (texture == null) return;
            materialPropertyBlock.SetTexture("_Texture", texture);
        }
    }
}