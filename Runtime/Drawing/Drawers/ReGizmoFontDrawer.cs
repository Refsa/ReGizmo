

using System.Runtime.InteropServices;
using ReGizmo.Core;
using UnityEngine;

namespace ReGizmo.Drawing
{
    internal class ReGizmoFontDrawer : ReGizmoDrawer<CharData>
    {
        protected override string PropertiesName { get; } = "_CharData";

        Font font;

        ComputeBuffer characterInfoBuffer;
        CharacterInfoShader[] characterInfos;

        public ReGizmoFontDrawer(Font font) : base()
        {
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/FontShader");
            this.font = font;
            renderArguments[1] = 1;

            SetupCharacterData();
        }

        void SetupCharacterData()
        {
            OnFontTextureChanged();

            characterInfos = new CharacterInfoShader[200];
            for (int i = 0; i < 200; i++)
            {
                if (!font.GetCharacterInfo((char)i, out var characterInfo)) continue;

                Vector4 size = new Vector4(characterInfo.minX, characterInfo.maxX, characterInfo.minY, characterInfo.maxY);

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
            material.SetBuffer("_CharacterInfos", characterInfoBuffer);
        }

        public ref CharacterInfoShader GetCharacterInfo(uint charIndex)
        {
            return ref characterInfos[charIndex];
        }

        public void OnFontTextureChanged()
        {
            if (font == null) return;

            material.SetTexture("_MainTex", font.material.mainTexture);
        }

        protected override void RenderInternal(Camera camera)
        {
            renderArguments[0] = CurrentDrawCount();
            renderArgumentsBuffer.SetData(renderArguments);

            Graphics.DrawProceduralIndirect(
                material, currentBounds,
                MeshTopology.Points,
                renderArgumentsBuffer, 0,
                camera,
                materialPropertyBlock
            );
        }

        protected override void SetMaterialPropertyBlockData()
        {
            base.SetMaterialPropertyBlockData();

            material.SetTexture("_MainTex", font.material.mainTexture);
            material.SetBuffer("_CharacterInfos", characterInfoBuffer);
        }

        public override void Dispose()
        {
            base.Dispose();
            characterInfoBuffer = ComputeBufferPool.Free(characterInfoBuffer);
        }
    }
}