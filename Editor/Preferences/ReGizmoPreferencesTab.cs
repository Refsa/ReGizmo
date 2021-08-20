using ReGizmo.Core.Fonts;
using ReGizmo.Drawing;
using ReGizmo.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.PackageManager.Requests;
using UnityEngine.Rendering;

namespace ReGizmo.Editor.Preferences
{
    internal static class ReGizmoPreferencesTab
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
                    keywords = new HashSet<string>(new[] { "ReGizmo", "Gizmos" }),
                    activateHandler = PreferencedTabUI
                };

            return provider;
        }

        static readonly VisualElement smallSpace = new VisualElement { style = { height = 25f } };

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

                    ReGizmoSettings.SetFont((Font)ce.newValue);
                    ReGizmoEditorUtils.SaveAsset(ReGizmoSettings.Instance);
                    ReGizmo.Core.ReGizmo.Initialize();
                    ReGizmo.Core.ReGizmo.SetActive(true);
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

                    ReGizmoSettings.SetSDFFont((ReSDFData)ce.newValue);
                    ReGizmoEditorUtils.SaveAsset(ReGizmoSettings.Instance);
                    ReGizmo.Core.ReGizmo.Initialize();
                    ReGizmo.Core.ReGizmo.SetActive(true);
                });
            }

            var fontSuperSampleToggle = new Toggle("Font Super Sampling");
            {
                fontSuperSampleToggle.value = ReGizmoSettings.FontSuperSample;
                fontSuperSampleToggle.RegisterValueChangedCallback(ce =>
                {
                    ReGizmoSettings.ToggleFontSuperSampling();
                    ReGizmoEditorUtils.SaveAsset(ReGizmoSettings.Instance);
                });
            }

            // SDF Font Setup Window
            var openSDFFontWindow = new Button(() => ReFontSetup.Open());
            openSDFFontWindow.text = "SDF Font Creator";

            var setAlphaBehindScale = new Slider(0f, 1f);
            setAlphaBehindScale.name = "Alpha Behind Scale";
            setAlphaBehindScale.label = "Alpha behind scale";
            setAlphaBehindScale.tooltip = "Control the alpha scale when rendering gizmos behind other objects";
            setAlphaBehindScale.value = ReGizmoSettings.AlphaBehindScale;
            setAlphaBehindScale.RegisterValueChangedCallback(ce =>
            {
                ReGizmoSettings.SetAlphaBehindScale(ce.newValue);
            });

#if REGIZMO_DEV
            var devSettingsLabel = new Label("Dev Settings");
            var showDebugGizmosButton = new Toggle("Show Debug Gizmos");
            {
                showDebugGizmosButton.value = ReGizmoSettings.ShowDebugGizmos;
                showDebugGizmosButton.RegisterValueChangedCallback(ce =>
                {
                    ReGizmoSettings.ToggleShowDebugGizmos();
                    ReGizmoEditorUtils.SaveAsset(ReGizmoSettings.Instance);
                });
            }


            // var targetPipelineDropdown = new DropdownMenu();
            // targetPipelineDropdown.AppendAction("Legacy", (dma) => { }, dma => dma.name == "Legacy" ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);
            // targetPipelineDropdown.AppendAction("HDRP", (dma) => { }, dma => dma.name == "HDRP" ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);
            // targetPipelineDropdown.AppendAction("URP", (dma) => { }, dma => dma.name == "URP" ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);
            var targetPipelineDropdown = new EnumField("Target Render Pipeline", ReGizmoEditor.CurrentPipeline);
            targetPipelineDropdown.RegisterValueChangedCallback(val =>
            {
                var pipeline = (RenderPipelineUtils.Pipeline)val.newValue;
                if (pipeline == ReGizmoEditor.CurrentPipeline) return;

                ChangePipeline(pipeline);
            });
#endif

            contentContainer.Add(enableRuntimeToggle);
            contentContainer.Add(setAlphaBehindScale);
            contentContainer.Add(defaultFontSelection);
            contentContainer.Add(defaultSDFFontSelection);
            contentContainer.Add(fontSuperSampleToggle);
            contentContainer.Add(openSDFFontWindow);

#if REGIZMO_DEV
            contentContainer.Add(devSettingsLabel);
            contentContainer.Add(showDebugGizmosButton);
            contentContainer.Add(targetPipelineDropdown);
#endif
        }

        static void Repaint()
        {
            SettingsService.OpenUserPreferences("Preferences/ReGizmo");
        }

#if REGIZMO_DEV
        static void ChangePipeline(RenderPipelineUtils.Pipeline pipeline)
        {
            ReGizmoEditor.ClearScriptDefines();

            List<System.Func<Request>> requests = new List<System.Func<Request>>();
            pipelineChange = pipeline;

            switch (pipeline)
            {
                case RenderPipelineUtils.Pipeline.Legacy:
                    requests.Add(() => UnityEditor.PackageManager.Client.Remove("com.unity.render-pipelines.high-definition"));
                    requests.Add(() => UnityEditor.PackageManager.Client.Remove("com.unity.render-pipelines.universal"));
                    break;
                case RenderPipelineUtils.Pipeline.HDRP:
                    requests.Add(() => UnityEditor.PackageManager.Client.Remove("com.unity.render-pipelines.universal"));
                    requests.Add(() => UnityEditor.PackageManager.Client.Add("com.unity.render-pipelines.high-definition"));
                    break;
                case RenderPipelineUtils.Pipeline.URP:
                    requests.Add(() => UnityEditor.PackageManager.Client.Remove("com.unity.render-pipelines.high-definition"));
                    requests.Add(() => UnityEditor.PackageManager.Client.Add("com.unity.render-pipelines.universal"));
                    break;
            }

            foreach (var request in requests)
            {
                var r = request.Invoke();
                if (r == null) continue;

                while (r.Status == UnityEditor.PackageManager.StatusCode.InProgress) continue;
                if (r.Status == UnityEditor.PackageManager.StatusCode.Failure)
                {
                    Debug.LogError(r.Error.errorCode + ": " + r.Error.message);
                    continue;
                }

                if (r is AddRequest)
                {
                    AwaitPipelineChange();
                }
            }
        }

        static RenderPipelineUtils.Pipeline pipelineChange;
        static void AwaitPipelineChange()
        {
            AssemblyReloadEvents.afterAssemblyReload -= AwaitPipelineChange;

            string pipelineAsset = null;

            switch (pipelineChange)
            {
                case RenderPipelineUtils.Pipeline.Legacy:
                    break;
                case RenderPipelineUtils.Pipeline.HDRP:
                    pipelineAsset = "HDRenderPipelineAsset";
                    break;
                case RenderPipelineUtils.Pipeline.URP:
                    pipelineAsset = "UniversalRenderPipelineAsset";
                    break;
            }

            if (string.IsNullOrEmpty(pipelineAsset))
            {
                GraphicsSettings.renderPipelineAsset = null;
            }
            else
            {
                var assets = AssetDatabase.FindAssets($"{pipelineAsset} t:{typeof(RenderPipelineAsset).Name}");
                if (assets != null && assets.Length > 0)
                {
                    var asset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(AssetDatabase.GUIDToAssetPath(assets[0]));
                    GraphicsSettings.renderPipelineAsset = asset;
                }
            }

            ReGizmoEditor.DetectPipeline();
        }
#endif
    }
}