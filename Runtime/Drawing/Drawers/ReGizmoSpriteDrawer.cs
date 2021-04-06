using UnityEngine;

namespace ReGizmo.Drawing
{
    internal struct SpriteShaderData
    {
        public Vector3 Position;
        public float Scale;
        public Vector4 UVs;
    }
    
    internal class ReGizmoSpriteDrawer : ReGizmoDrawer<SpriteShaderData>
    {
        Sprite sprite;
        
        public ReGizmoSpriteDrawer(Sprite sprite)
        {
            this.sprite = sprite;
            
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Sprite");
            renderArguments[1] = 1;
        }
        
        protected override void RenderInternal(Camera camera)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            Graphics.DrawProceduralIndirect(
                material, currentBounds, MeshTopology.Points,
                renderArgumentsBuffer, 0,
                camera, materialPropertyBlock);
        }
        
        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();
            
            materialPropertyBlock.SetTexture("_SpriteTexture", sprite.texture);
        }
    }
}