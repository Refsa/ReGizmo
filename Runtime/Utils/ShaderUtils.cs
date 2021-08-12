using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo
{
    internal static class ShaderUtils
    {
        static Material clearMaterial;
        static Material clearDepthMaterial;
        static Material clearWithDepthMaterial;
        static Material copyDepthMaterial;

        static ShaderUtils()
        {
            clearMaterial = new Material(Shader.Find("Hidden/ReGizmo/Clear"));
            clearDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/ClearDepth"));
            clearWithDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/ClearWithDepth"));
            copyDepthMaterial = new Material(Shader.Find("Hidden/ReGizmo/CopyDepth"));
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

        public static void ClearDepth(CommandBuffer cmd, RenderTargetIdentifier texture, float depth)
        {
            cmd.SetGlobalFloat("_ClearDepth", depth);
            cmd.Blit(null, texture, clearDepthMaterial);
        }

        public static void CopyDepth(CommandBuffer cmd, RenderTargetIdentifier targetTexture, RenderTargetIdentifier depthTexture)
        {
            cmd.SetGlobalTexture("_MainTex", depthTexture);
            cmd.Blit(depthTexture, targetTexture, copyDepthMaterial);
        }
    }
}