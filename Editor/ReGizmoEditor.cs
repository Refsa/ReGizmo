using System;
using ReGizmo.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Editor
{
    [InitializeOnLoad]
    static class ReGizmoEditor
    {
        static double startTime;
        static EventType lastGUIEventType;

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

            SceneView.duringSceneGui += OnSceneGUI;
        }


        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild -= OnAfterBuild;
            BuildHook.onBeforeBuild -= OnBeforeBuild;

            SceneView.duringSceneGui -= OnSceneGUI;

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

        private static void OnSceneGUI(SceneView obj)
        {
            lastGUIEventType = Event.current.type;
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
            Core.ReGizmo.SetActive(false);
            Core.ReGizmo.Dispose();
        }

        static void OnAfterAssemblyReloaded()
        {
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