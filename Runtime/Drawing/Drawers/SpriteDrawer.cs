using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct SpriteShaderData
    {
        public Vector3 Position;
        public float Scale;
        public Vector4 UVs;
    }

    internal class SpriteDrawer : ReGizmoDrawer<SpriteShaderData>
    {
        Sprite sprite;
        Vector4 spriteUVs;
        Vector2Int oldSpriteSize;

        protected override string PropertiesName { get; } = "_DrawData";

        public Vector4 SpriteUVs => spriteUVs;

        public SpriteDrawer(Sprite sprite)
        {
            this.sprite = sprite;

            SetupSpriteUVs();

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Sprite");

            cullingHandler = new SpriteCullingHandler();
        }

        void SetupSpriteUVs()
        {
            Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Rect spriteRect = sprite.textureRect;
            spriteRect.position /= spriteSize;
            spriteRect.size /= spriteSize;

            spriteUVs = new Vector4(
                spriteRect.xMin, spriteRect.yMin, spriteRect.xMax, spriteRect.yMax
            );

            oldSpriteSize.x = sprite.texture.width;
            oldSpriteSize.y = sprite.texture.height;
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth)
        {
            if (oldSpriteSize.x != sprite.texture.width || oldSpriteSize.y != sprite.texture.height)
            {
                SetupSpriteUVs();
            }

            uniqueDrawData.SetInstanceCount(1);
            uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData(MaterialPropertyBlock materialPropertyBlock)
        {
            base.SetMaterialPropertyBlockData(materialPropertyBlock);

            materialPropertyBlock.SetTexture("_SpriteTexture", sprite.texture);
        }
    }
}