using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal static class ShaderUtils
    {
        static Material clearMaterial;

        static ShaderUtils()
        {
            clearMaterial = new Material(Shader.Find("Hidden/ReGizmo/Clear"));
        }

        public static void ClearTexture(CommandBuffer cmd, RenderTargetIdentifier texture, Color clearColor)
        {
            cmd.SetGlobalColor("_ClearColor", clearColor);
            cmd.Blit(null, texture, clearMaterial);
        }

        public static void ClearTexture(RenderTexture texture, Color clearColor)
        {
            clearMaterial.SetColor("_ClearColor", clearColor);
            Graphics.Blit(null, texture, clearMaterial);
        }
    }
}