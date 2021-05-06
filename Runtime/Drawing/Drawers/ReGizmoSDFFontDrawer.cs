using System.Runtime.InteropServices;
using ReGizmo.Core;
using ReGizmo.Core.Fonts;
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
            renderArguments[1] = 1;

            SetupCharacterData();
        }

        void SetupCharacterData()
        {
            Vector2 atlasTextureSize = new Vector2(font.Font.atlas.width, font.Font.atlas.height);

            characterInfos = new CharacterInfoShader[200];
            for (int i = 0; i < 200; i++)
            {
                if (!font.TryGetGlyph((char) i, out var glyph)) continue;

                Vector4 size = new Vector4(
                    glyph.planeBounds.left, glyph.planeBounds.right,
                    glyph.planeBounds.bottom, glyph.planeBounds.top
                );

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
            id = (uint) textDataBuffers.Count();
            return ref textDataBuffers.Get();
        }
 
        protected override void RenderInternal(CommandBuffer cmd)
        {
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

            material.SetFloat("_DistanceRange", font.Font.atlas.distanceRange);
            material.SetVector("_AtlasDimensions", new Vector2(font.Font.atlas.width, font.Font.atlas.height));
            material.SetFloat("_AtlasSize", font.Font.atlas.size);
            material.SetTexture("_MainTex", font.Texture);
            material.SetBuffer("_CharacterInfos", characterInfoBuffer);
            textDataBuffers.PushData(materialPropertyBlock, "_TextData");
        }

        public override void Dispose()
        {
            base.Dispose();
            textDataBuffers.Dispose();
            characterInfoBuffer = ComputeBufferPool.Free(characterInfoBuffer);
        }
    }
}