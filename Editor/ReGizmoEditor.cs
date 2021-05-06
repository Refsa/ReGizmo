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
        static double startTime;
        static bool isSetup;

        [InitializeOnLoadMethod]
        static void Init()
        {
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
            Core.ReGizmo.DetectPipeline();
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
                Core.ReGizmo.Dispose();
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                SceneView.duringSceneGui -= OnDuringSceneGUI;
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            Core.ReGizmo.Dispose();
        }

        static void OnAfterAssemblyReloaded()
        {
        }

        static void OnSceneChanged(Scene arg0, Scene arg1)
        {
            Init();
        }

        static void AwaitSetup()
        {
            if (EditorApplication.timeSinceStartup - startTime > 0.25)
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
    }
}