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
        Vector4 spriteUVs;

        public Vector4 SpriteUVs => spriteUVs;

        public ReGizmoSpriteDrawer(Sprite sprite)
        {
            this.sprite = sprite;

            Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Rect spriteRect = sprite.textureRect;
            spriteRect.position /= spriteSize;
            spriteRect.size /= spriteSize;

            spriteUVs = new Vector4(
                spriteRect.xMin, spriteRect.yMin, spriteRect.xMax, spriteRect.yMax
            );

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