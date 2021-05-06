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

    internal class ReGizmoSpriteDrawer : ReGizmoDrawer<SpriteShaderData>
    {
        Sprite sprite;
        Vector4 spriteUVs;
        Vector2Int oldSpriteSize;

        protected override string PropertiesName { get; } = "_DrawData";

        public Vector4 SpriteUVs => spriteUVs;

        public ReGizmoSpriteDrawer(Sprite sprite)
        {
            this.sprite = sprite;

            SetupSpriteUVs();

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Sprite");
            renderArguments[1] = 1;
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

        protected override void RenderInternal(CommandBuffer cmd)
        {
            if (oldSpriteSize.x != sprite.texture.width || oldSpriteSize.y != sprite.texture.height)
            {
                SetupSpriteUVs();
            }

            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 0,
                MeshTopology.Points,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, 1,
                MeshTopology.Points,
                renderArgumentsBuffer, 0,
                materialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();

            materialPropertyBlock.SetTexture("_SpriteTexture", sprite.texture);
        }
    }
}