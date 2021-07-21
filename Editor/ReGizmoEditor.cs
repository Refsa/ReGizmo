using System;
using ReGizmo.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ReGizmo.Editor
{
    static class ReGizmoEditor
    {
        static RenderPipelineUtils.Pipeline currentPipeline;
        static double startTime;
        static bool isSetup;
        static bool isBuilding;

        public static RenderPipelineUtils.Pipeline CurrentPipeline => currentPipeline;

        [InitializeOnLoadMethod]
        static void Init()
        {
            DetectPipeline();

            DeAttachEventHooks();
            AttachEventHooks();

            HookAwaitSetup();
        }

        static void AttachEventHooks()
        {
            EditorApplication.playModeStateChanged += OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild += OnAfterBuild;
            BuildHook.onBeforeBuild += OnBeforeBuild;

            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChangedInEditMode;

            EditorApplication.projectChanged += OnProjectChanged;
        }

        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild -= OnAfterBuild;
            BuildHook.onBeforeBuild -= OnBeforeBuild;

            EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChangedInEditMode;

            EditorApplication.update -= AwaitSetup;
            SceneView.duringSceneGui -= OnDuringSceneGUI;

            EditorApplication.projectChanged -= OnProjectChanged;
        }

        private static void OnProjectChanged()
        {
            DetectPipeline();
        }

        static void OnBeforeBuild()
        {
            Core.ReGizmo.Dispose();
            isBuilding = true;
        }

        static void OnAfterBuild()
        {
            isBuilding = false;
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                Core.ReGizmo.Dispose();
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                Core.ReGizmo.SetActive(true);
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
                Core.ReGizmo.Dispose();
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {

            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            Core.ReGizmo.Dispose();
        }

        static void OnAfterAssemblyReloaded()
        {

        }

        static void OnSceneChangedInEditMode(Scene arg0, Scene arg1)
        {
            if (isBuilding) return;

            ReGizmo.Core.ReGizmo.Initialize();
            ReGizmo.Core.ReGizmo.SetActive(true);
        }

        static void HookAwaitSetup()
        {
            startTime = EditorApplication.timeSinceStartup;
            if (isSetup)
            {
                EditorApplication.update -= AwaitSetup;
                isSetup = false;
            }
            EditorApplication.update += AwaitSetup;
        }

        static void AwaitSetup()
        {
            if (isSetup)
            {
                EditorApplication.update -= AwaitSetup;
                return;
            }

            if (EditorApplication.timeSinceStartup - startTime > 0.25)
            {
                ReGizmo.Core.ReGizmo.Initialize();

                SceneView.duringSceneGui += OnDuringSceneGUI;
                EditorApplication.update -= AwaitSetup;

                isSetup = true;
            }
        }

        static void OnDuringSceneGUI(SceneView sceneView)
        {
            if (Event.current.type != EventType.Repaint) return;

            OnUpdate();
        }

        static void OnUpdate()
        {
            if (!isSetup) return;

            Core.ReGizmo.OnUpdate();
        }

        public static void DetectPipeline()
        {
            currentPipeline = RenderPipelineUtils.DetectPipeline();

            switch (currentPipeline)
            {
                case RenderPipelineUtils.Pipeline.Unknown:
                    break;
                case RenderPipelineUtils.Pipeline.Legacy:
                    Shader.EnableKeyword(RenderPipelineUtils.LegacyKeyword);
                    Shader.DisableKeyword(RenderPipelineUtils.SRPKeyword);
                    break;
                case RenderPipelineUtils.Pipeline.HDRP:
                case RenderPipelineUtils.Pipeline.URP:
                    Shader.EnableKeyword(RenderPipelineUtils.SRPKeyword);
                    Shader.DisableKeyword(RenderPipelineUtils.LegacyKeyword);
                    break;
            }

            UpdateScriptDefines();
        }

        public static void ClearScriptDefines()
        {
            SetScriptDefines(old =>
            {
                return old
                    .Replace(RenderPipelineUtils.LegacyKeyword, "")
                    .Replace(RenderPipelineUtils.URPKeyword, "")
                    .Replace(RenderPipelineUtils.HDRPKeyword, "")
                    .Replace(RenderPipelineUtils.SRPKeyword, "");
            });
        }

        static void UpdateScriptDefines()
        {
            SetScriptDefines(old =>
            {
                if (old.Contains(currentPipeline.GetDefine()))
                {
                    return old;
                }

                old = old
                    .Replace(RenderPipelineUtils.LegacyKeyword, "")
                    .Replace(RenderPipelineUtils.URPKeyword, "")
                    .Replace(RenderPipelineUtils.HDRPKeyword, "")
                    .Replace(RenderPipelineUtils.SRPKeyword, "");

                old += $";{currentPipeline.GetDefine()}";
                return old;
            });
        }

        static void SetScriptDefines(Func<string, string> predicate)
        {
            string defines = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.BuildTargetGroup.Standalone);
            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(UnityEditor.BuildTargetGroup.Standalone, predicate.Invoke(defines));
        }

#if RG_HDRP
        [MenuItem("Window/ReGizmo/HDRP Scene Setup")]
        static void SetupHDRPProxy()
        {
            if (GameObject.Find("ReGizmo_HDRP_Proxy")) return;

            var assets = AssetDatabase.FindAssets($"ReGizmo_HDRP_Proxy");
            if (assets != null && assets.Length > 0)
            {
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assets[0]));
                var go = GameObject.Instantiate(asset);
                go.name = "ReGizmo_HDRP_Proxy";
                Debug.Log("Added ReGizmo HDRP Proxy object to scene");
            }
            else
            {
                Debug.LogError("Couldn't find ReGizmo HDRP Proxy prefab");
            }
        }
#endif
    }
}