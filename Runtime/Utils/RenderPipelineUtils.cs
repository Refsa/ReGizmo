using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace ReGizmo.Utils
{
    internal static class RenderPipelineUtils
    {
        public const string LegacyKeyword = "RG_LEGACY";
        public const string SRPKeyword = "RG_SRP";
        public const string URPKeyword = "RG_SRP;RG_URP";
        public const string HDRPKeyword = "RG_SRP;RG_HDRP";

        public enum Pipeline
        {
            Unknown,
            Legacy,
            HDRP,
            URP,
        }

        public static Pipeline DetectPipeline()
        {
#if UNITY_2019_1_OR_NEWER
            if (GraphicsSettings.renderPipelineAsset != null)
            {
                // SRP
                var srpType = GraphicsSettings.renderPipelineAsset.GetType().ToString();
                if (srpType.Contains("HDRenderPipelineAsset"))
                {
                    return Pipeline.HDRP;
                }
                else if (srpType.Contains("UniversalRenderPipelineAsset") || srpType.Contains("LightweightRenderPipelineAsset"))
                {
                    return Pipeline.URP;
                }
                else return Pipeline.Unknown;
            }
#elif UNITY_2017_1_OR_NEWER
            if (GraphicsSettings.renderPipelineAsset != null) {
                return Pipeline.Unknown;
            }
#endif
            // no SRP
            return Pipeline.Legacy;
        }

        public static string GetDefine(this Pipeline pipeline)
        {
            switch (pipeline)
            {
                case Pipeline.Unknown: return "";
                case Pipeline.Legacy: return LegacyKeyword;
                case Pipeline.URP: return URPKeyword;
                case Pipeline.HDRP: return HDRPKeyword;
            }

            return "";
        }
    }
}