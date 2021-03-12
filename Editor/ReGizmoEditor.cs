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

            SceneView.duringSceneGui -= OnSceneGUI;

            EditorApplication.update -= OnUpdate;
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
                UnityEngine.Debug.Log($"exit edit mode");
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                UnityEngine.Debug.Log($"enter edit mode");
                ReGizmo.Core.ReGizmo.Initialize();
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
                UnityEngine.Debug.Log($"exit play mode");
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                UnityEngine.Debug.Log($"enter play mode");
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            UnityEngine.Debug.Log($"before assembly reload");
            // if (lastGUIEventType != EventType.Repaint)
            // if (!exitingEditMode)
            // {
            //     ReGizmo.Core.ReGizmo.Interrupt();
            //     ReGizmo.Core.ReGizmo.Dispose();
            // }
        }

        static void OnAfterAssemblyReloaded()
        {
            UnityEngine.Debug.Log($"after assembly reload");
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
                Console.WriteLine("Changing mode");
            }

            if (!exitingEditMode)
            {
                // ReGizmo.Core.ReGizmo.OnUpdate();
            }
        }
    }
}