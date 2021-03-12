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
        static bool exitingEditMode;
        static EventType lastGUIEventType;

        static ReGizmoEditor()
        {
            startTime = EditorApplication.timeSinceStartup;
            DeAttachEventHooks();
            AttachEventHooks();
        }

        static void AttachEventHooks()
        {
            EditorApplication.update += AwaitSetup;

            EditorApplication.playModeStateChanged += OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReloaded;

            BuildHook.onAfterBuild += OnAfterBuild;
            BuildHook.onBeforeBuild += OnBeforeBuild;

            SceneView.duringSceneGui += OnSceneGUI;

            if (!Application.isPlaying)
            {
                EditorApplication.update += OnUpdate;
            }
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

            if (exitingEditMode)
            {
                ReGizmo.Core.ReGizmo.Interrupt();
                if (Event.current.type != EventType.Repaint)
                {
                    ReGizmo.Core.ReGizmo.Dispose();
                }
            }
            else if (Event.current.type == EventType.Repaint)
            {
                ReGizmo.Core.ReGizmo.OnUpdate();
            }
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                exitingEditMode = true;
                ReGizmo.Core.ReGizmo.Interrupt();
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                ReGizmo.Core.ReGizmo.Initialize();
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
            // if (lastGUIEventType != EventType.Repaint)
            // if (!exitingEditMode)
            // {
            //     ReGizmo.Core.ReGizmo.Interrupt();
            //     ReGizmo.Core.ReGizmo.Dispose();
            // }
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
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
            }

            if (!exitingEditMode)
            {
                // ReGizmo.Core.ReGizmo.OnUpdate();
            }
        }
    }
}