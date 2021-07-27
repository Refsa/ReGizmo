using ReGizmo.Core.Fonts;
using UnityEngine;

namespace ReGizmo.Core
{
    public class ReGizmoSettings : ScriptableObject
    {
        const string SettingsKey = "regizmo.settings";

        static ReGizmoSettings instance;
        public static ReGizmoSettings Instance => instance;

        [SerializeField] Font font;
        [SerializeField] ReSDFData sdfFont;
        [SerializeField] bool fontSuperSample = true;
        [SerializeField] float alphaBehindScale = 0.5f;

#if REGIZMO_DEV
        [SerializeField] bool showDebugGizmos = true;
#endif

        public static Font Font => instance.font;
        public static ReSDFData SDFFont => instance.sdfFont;
        public static bool FontSuperSample => instance.fontSuperSample;
        public static bool ShowDebugGizmos => instance.showDebugGizmos;
        public static float AlphaBehindScale => instance.alphaBehindScale;

        public static void SetFont(Font font)
        {
            if (font != null)
            {
                instance.font = font;
            }
        }

        public static void SetSDFFont(ReSDFData font)
        {
            if (font != null)
            {
                instance.sdfFont = font;
            }
        }

        public static void SetAlphaBehindScale(float alphaBehindScale)
        {
            instance.alphaBehindScale = alphaBehindScale;
        }

        public static void ToggleFontSuperSampling()
        {
            instance.fontSuperSample = !instance.fontSuperSample;
        }

        public static void ToggleShowDebugGizmos()
        {
            instance.showDebugGizmos = !instance.showDebugGizmos;
        }

        public static void Load()
        {
            if (instance != null) return;

            instance = ReGizmoHelpers.LoadAssetByName<ReGizmoSettings>("ReGizmoSettings");
        }

        void OnValidate()
        {
            if (font == null)
            {
                font = ReGizmoHelpers.LoadAssetByName<Font>("Inter-Medium");
            }

            if (sdfFont == null)
            {
                sdfFont = ReGizmoHelpers.LoadAssetByName<ReSDFData>("Inter-Medium");
            }
        }
    }
}