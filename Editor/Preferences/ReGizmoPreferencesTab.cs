using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using ReGizmo.Core.Fonts;
using ReGizmo.Drawing;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReGizmo.Editor.Preferences
{
    public static class ReGizmoPreferencesTab
    {
        [MenuItem("Window/ReGizmo/Preferences", false, 1)]
        public static void ReGizmoPreferences() => SettingsService.OpenUserPreferences("Preferences/ReGizmo");

        [SettingsProvider]
        public static SettingsProvider GetPreferencesTab()
        {
            var provider =
                new SettingsProvider("Preferences/ReGizmo", SettingsScope.User)
                {
                    label = "ReGizmo",
                    keywords = new HashSet<string>(new[] {"ReGizmo", "Gizmos"}),
                    activateHandler = PreferencedTabUI
                };
            
            return provider;
        }

        static readonly VisualElement smallSpace = new VisualElement {style = {height = 25f}};

        static void PreferencedTabUI(string context, VisualElement root)
        {
            ReGizmoSettings.Load();

            var container = new ScrollView(ScrollViewMode.Vertical);
            root.Add(container);

            // HEADER
            var headerContainer = new VisualElement
            {
                style =
                {
                    fontSize = 20, unityFontStyleAndWeight = FontStyle.Bold, marginLeft = 15, marginTop = 5,
                    flexDirection = FlexDirection.Column, unityTextAlign = TextAnchor.MiddleCenter
                }
            };
            {
                var header = new Label("ReGizmo Settings");
                header.style.borderBottomWidth = 2f;
                header.style.borderBottomColor = ReColors.ORANGE;
                var logo = new Image();
                logo.scaleMode = ScaleMode.ScaleToFit;
                logo.image = ReGizmoEditorUtils.LogoTexture;
                headerContainer.Add(logo);
                headerContainer.Add(header);
            }

            // Content
            var contentContainer =
                new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Column
                    }
                };

            container.Add(headerContainer);
            container.Add(smallSpace);
            container.Add(contentContainer);

            // Enable runtime toggle
            var enableRuntimeToggle = new Toggle("Enable Runtime");
            {
                enableRuntimeToggle.value = ReGizmoEditorUtils.RuntimeEnabled;
                enableRuntimeToggle.RegisterValueChangedCallback(ce =>
                {
                    ReGizmoEditorUtils.ToggleRuntimeScriptDefine();
                });
            }

            // Font selection
            var defaultFontSelection = new ObjectField("Default Font");
            {
                defaultFontSelection.objectType = typeof(Font);
                defaultFontSelection.value = ReGizmoSettings.Font;
                defaultFontSelection.RegisterValueChangedCallback(ce =>
                {
                    if (ce.newValue == null)
                    {
                        defaultFontSelection.value = ReGizmoSettings.Font;
                        return;
                    }

                    ReGizmoSettings.SetFont((Font) ce.newValue);
                });
            }

            // SDF Font selection
            var defaultSDFFontSelection = new ObjectField("Default SDF Font");
            {
                defaultSDFFontSelection.objectType = typeof(ReSDFData);
                defaultSDFFontSelection.value = ReGizmoSettings.SDFFont;
                defaultSDFFontSelection.RegisterValueChangedCallback(ce =>
                {
                    if (ce.newValue == null)
                    {
                        defaultSDFFontSelection.value = ReGizmoSettings.SDFFont;
                        return;
                    }

                    ReGizmoSettings.SetSDFFont((ReSDFData) ce.newValue);
                });
            }

            // SDF Font Setup Window
            var openSDFFontWindow = new Button(() => ReFontSetup.Open());
            openSDFFontWindow.text = "SDF Font Creator";

            contentContainer.Add(enableRuntimeToggle);
            contentContainer.Add(defaultFontSelection);
            contentContainer.Add(defaultSDFFontSelection);
            contentContainer.Add(openSDFFontWindow);
        }

        static void Repaint()
        {
            SettingsService.OpenUserPreferences("Preferences/ReGizmo");
        }
    }
}