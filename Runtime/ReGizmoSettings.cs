using System;
using System.Collections;
using System.Collections.Generic;
using ReGizmo.Core.Fonts;
using UnityEditor;
using UnityEngine;

namespace ReGizmo
{
    public class ReGizmoSettings : ScriptableObject
    {
        const string SettingsKey = "regizmo.settings";

        static ReGizmoSettings instance;
        public static ReGizmoSettings Instance => instance;

        [SerializeField] Font font;
        [SerializeField] ReSDFData sdfFont;

        public static Font Font => instance.font;
        public static ReSDFData SDFFont => instance.sdfFont;

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

        public static void Load()
        {
            if (instance != null) return;

            instance = ReGizmoHelpers.LoadAssetByName<ReGizmoSettings>("ReGizmoSettings");

            Core.ReGizmo.DetectPipeline();
        }

        void OnValidate()
        {
            if (font == null)
            {
                font = ReGizmoHelpers.LoadAssetByName<Font>("secrcode");
            }

            if (sdfFont == null)
            {
                sdfFont = ReGizmoHelpers.LoadAssetByName<ReSDFData>("secrcode");
            }
        }
    }
}