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
        static bool toggle;

        static ReGizmoEditor()
        {
            startTime = EditorApplication.timeSinceStartup;

            ReGizmo.Core.ReGizmo.Dispose();
            DeAttachEventHooks();
            AttachEventHooks();
        }

        static void AttachEventHooks()
        {
            EditorApplication.update += AwaitSetup;

            EditorApplication.playModeStateChanged += OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReloaded;

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

            EditorApplication.update -= OnUpdate;
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                UnityEngine.Debug.Log($"exit edit mode");
                ReGizmo.Core.ReGizmo.Dispose();
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

            ReGizmo.Core.ReGizmo.OnUpdate();
        }
    }
} 