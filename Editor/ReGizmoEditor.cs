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
        }

        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild -= OnAfterBuild;
            BuildHook.onBeforeBuild -= OnBeforeBuild;

            EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;

            EditorApplication.update -= OnUpdate;
        }

        static void OnBeforeBuild()
        {
        }

        static void OnAfterBuild()
        {
            DeAttachEventHooks();
            AttachEventHooks();
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
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            //Core.ReGizmo.SetActive(false);
            //Core.ReGizmo.Dispose();
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
            if (EditorApplication.timeSinceStartup - startTime > 1.0)
            {
                ReGizmo.Core.ReGizmo.Initialize();

                EditorApplication.update -= AwaitSetup;
                EditorApplication.update += OnUpdate;

                isSetup = true;
            }
        }

        static void OnUpdate()
        {
            if (!isSetup) return;

            Core.ReGizmo.OnUpdate();
        }
    }
}