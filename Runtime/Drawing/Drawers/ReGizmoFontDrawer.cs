using ReGizmo.Core;
using ReGizmo.Utils;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class ReGizmoFontDrawer : ReGizmoDrawer<CharData>
    {
        protected override string PropertiesName { get; } = "_CharData";

        Font font;

        ComputeBuffer characterInfoBuffer;
        CharacterInfoShader[] characterInfos;

        ShaderDataBuffer<TextData> textDataBuffers;

        public ReGizmoFontDrawer(Font font) : base()
        {
            textDataBuffers = new ShaderDataBuffer<TextData>();

            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Font");
            this.font = font;
            renderArguments[1] = 1;

            SetupCharacterData();
        }

        void SetupCharacterData()
        {
            characterInfos = new CharacterInfoShader[200];
            for (int i = 0; i < 200; i++)
            {
                if (!font.GetCharacterInfo((char)i, out var characterInfo)) continue;

                Vector4 size = new Vector4(
                    characterInfo.minX, characterInfo.maxX,
                    characterInfo.minY, characterInfo.maxY);

                /* Vector4 size = new Vector4(
                    (characterInfo.minX + characterInfo.maxX) * 0.5f,
                    (characterInfo.minY + characterInfo.maxY) * 0.5f,
                    0f, 0f); */

                var ci = new CharacterInfoShader
                {
                    BottomLeft = characterInfo.uvBottomLeft,
                    BottomRight = characterInfo.uvBottomRight,
                    TopLeft = characterInfo.uvTopLeft,
                    TopRight = characterInfo.uvTopRight,
                    Size = size / font.fontSize,
                    Advance = (float)characterInfo.advance / font.fontSize
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
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();

            materialPropertyBlock.SetTexture("_MainTex", font.material.mainTexture);
            materialPropertyBlock.SetBuffer("_CharacterInfos", characterInfoBuffer);

            textDataBuffers.PushData();
            materialPropertyBlock.SetBuffer("_TextData", textDataBuffers.ComputeBuffer);
        }

        public override void Dispose()
        {
            base.Dispose();
            textDataBuffers.Dispose();
            characterInfoBuffer = ComputeBufferPool.Free(characterInfoBuffer);
        }
    }
}