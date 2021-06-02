using System.Runtime.InteropServices;
using ReGizmo.Core;
using ReGizmo.Core.Fonts;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoSDFFontDrawer : ReGizmoDrawer<CharData>
    {
        protected override string PropertiesName { get; } = "_CharData";

        ReSDFData font;

        ComputeBuffer characterInfoBuffer;
        CharacterInfoShader[] characterInfos;

        ShaderDataBuffer<TextData> textDataBuffers;

        public ReGizmoSDFFontDrawer(ReSDFData font) : base()
        {
            textDataBuffers = new ShaderDataBuffer<TextData>();

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Font_SDF");
            this.font = font;
            
            SetupCharacterData();

            // cullingHandler = new FontCullingHandler();
        }

        void SetupCharacterData()
        {
            Vector2 atlasTextureSize = new Vector2(font.Font.atlas.width, font.Font.atlas.height);

            characterInfos = new CharacterInfoShader[200];
            for (int i = 0; i < 200; i++)
            {
                if (!font.TryGetGlyph((char)i, out var glyph)) continue;

                Vector4 size = new Vector4(
                    glyph.planeBounds.left, glyph.planeBounds.right,
                    glyph.planeBounds.bottom, glyph.planeBounds.top
                );

                /* Vector4 size = new Vector4(
                    (glyph.planeBounds.left + glyph.planeBounds.right) * 0.5f,
                    (glyph.planeBounds.top + glyph.planeBounds.bottom) * 0.5f,
                    0f, 0f); */

                Vector2 bottomLeftUV = new Vector2(glyph.atlasBounds.left, glyph.atlasBounds.bottom) /
                                       atlasTextureSize;
                Vector2 bottomRightUV =
                    new Vector2(glyph.atlasBounds.right, glyph.atlasBounds.bottom) / atlasTextureSize;
                Vector2 topLeftUV = new Vector2(glyph.atlasBounds.left, glyph.atlasBounds.top) / atlasTextureSize;
                Vector2 topRightUV = new Vector2(glyph.atlasBounds.right, glyph.atlasBounds.top) / atlasTextureSize;

                var ci = new CharacterInfoShader
                {
                    BottomLeft = bottomLeftUV,
                    BottomRight = bottomRightUV,
                    TopLeft = topLeftUV,
                    TopRight = topRightUV,
                    Size = size,
                    Advance = glyph.advance
                };

                characterInfos[i] = ci;
            }

            ComputeBufferPool.Free(characterInfoBuffer);
            characterInfoBuffer = ComputeBufferPool.Get(200, Marshal.SizeOf<CharacterInfoShader>());
            characterInfoBuffer.SetData(characterInfos);
        }

        public override void Clear()
        {
            base.Clear();
            textDataBuffers.Reset();
        }

        public ref CharacterInfoShader GetCharacterInfo(uint charIndex)
        {
            return ref characterInfos[charIndex];
        }

        public ref TextData GetTextShaderData(out uint id)
        {
            id = (uint)textDataBuffers.Count();
            return ref textDataBuffers.Get();
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData)
        {
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

            materialPropertyBlock.SetBuffer("_CharacterInfos", characterInfoBuffer);
            textDataBuffers.PushData();
            materialPropertyBlock.SetBuffer("_TextData", textDataBuffers.ComputeBuffer);

            materialPropertyBlock.SetFloat("_DistanceRange", font.Font.atlas.distanceRange);
            materialPropertyBlock.SetVector("_AtlasDimensions", new Vector2(font.Font.atlas.width, font.Font.atlas.height));
            materialPropertyBlock.SetFloat("_AtlasSize", font.Font.atlas.size);

            materialPropertyBlock.SetTexture("_MainTex", font.GetTexture());
        }

        protected override void SetCullingData()
        {
            // var fontCullingData = (FontCullingHandler)cullingHandler;
            // fontCullingData.SetData(textDataBuffers.ComputeBuffer);
        }

        public override void Dispose()
        {
            base.Dispose();
            textDataBuffers.Dispose();
            characterInfoBuffer = ComputeBufferPool.Free(characterInfoBuffer);
        }
    }
}