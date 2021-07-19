using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal static class ShaderUtils
    {
        static Material clearMaterial;
        static Material clearWithDepthMaterial;

        static ShaderUtils()
        {
            clearMaterial = new Material(Shader.Find("Hidden/ReGizmo/Clear"));
            clearWithDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/ClearWithDepth"));
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

        // method ClearWithDepth that clears color and depth buffer
        public static void ClearWithDepth(CommandBuffer cmd, RenderTargetIdentifier texture, Color clearColor, float depth)
        {
            cmd.SetGlobalColor("_ClearColor", clearColor);
            cmd.SetGlobalFloat("_ClearDepth", depth);
            cmd.Blit(null, texture, clearWithDepthMaterial);
        }
    }
}