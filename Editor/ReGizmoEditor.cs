using System;
using ReGizmo.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ReGizmo.Editor
{
    [InitializeOnLoad]
    static class ReGizmoEditor
    {
        static double startTime;

        static ReGizmoEditor()
        {
            DeAttachEventHooks();

            startTime = EditorApplication.timeSinceStartup;

            AttachEventHooks();
        }

        static void AttachEventHooks()
        {
            EditorApplication.update += AwaitSetup;
            EditorApplication.update += OnUpdate;

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
                DeAttachEventHooks();
                AttachEventHooks();
                ReGizmo.Core.ReGizmo.Initialize();
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                EditorApplication.update -= OnUpdate;
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
            Core.ReGizmo.Reload();
        }

        static void AwaitSetup()
        {
            if (EditorApplication.timeSinceStartup - startTime > 1)
            {
                EditorApplication.update -= AwaitSetup;
                ReGizmo.Core.ReGizmo.Initialize();
            }
        }

        static void OnUpdate()
        {
            Core.ReGizmo.OnUpdate();
        }
    }
}