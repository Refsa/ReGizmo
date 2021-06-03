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

        [InitializeOnLoadMethod]
        static void Init()
        {
            DetectPipeline();

            DeAttachEventHooks();
            AttachEventHooks();

            startTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += AwaitSetup;
        }

        static void AttachEventHooks()
        {
            EditorApplication.playModeStateChanged += OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild += OnAfterBuild;
            BuildHook.onBeforeBuild += OnBeforeBuild;

            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;

            EditorApplication.projectChanged += OnProjectChanged;
        }

        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild -= OnAfterBuild;
            BuildHook.onBeforeBuild -= OnBeforeBuild;

            EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;

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
        }

        static void OnAfterBuild()
        {
            Init();
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
                Core.ReGizmo.Dispose();
                ComputeBufferPool.FreeAll(); 
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                SceneView.duringSceneGui -= OnDuringSceneGUI;
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            Core.ReGizmo.Dispose();
            ComputeBufferPool.FreeAll();
        }
  
        static void OnAfterAssemblyReloaded()
        {
        }

        static void OnSceneChanged(Scene arg0, Scene arg1)
        {
            ComputeBufferPool.FreeAll();
            Core.ReGizmo.Dispose();
            Init();
        }

        static void AwaitSetup()
        {
            if (EditorApplication.timeSinceStartup - startTime > 0.5)
            {
                ReGizmo.Core.ReGizmo.Initialize();

                EditorApplication.update -= AwaitSetup;
                SceneView.duringSceneGui += OnDuringSceneGUI;

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

        static void UpdateScriptDefines()
        {
            string defines = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.BuildTargetGroup.Standalone);
            if (defines.Contains(currentPipeline.GetDefine()))
            {
                return;
            }

            defines = defines
                .Replace(RenderPipelineUtils.LegacyKeyword, "")
                .Replace(RenderPipelineUtils.URPKeyword, "")
                .Replace(RenderPipelineUtils.HDRPKeyword, "")
                .Replace(RenderPipelineUtils.SRPKeyword, "");

            defines += $";{currentPipeline.GetDefine()}";

            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(UnityEditor.BuildTargetGroup.Standalone, defines);
        }
    }
}